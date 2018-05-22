using System;
using System.Linq;
using System.Windows.Forms;
using SourceCode.Tools.Management.DataAccessLayer;
using SourceCode.Tools.Management.Extensions;
using SourceCode.Tools.Management.Factories;
using SourceCode.Tools.Management.Helpers;
using SourceCode.Tools.Management.Properties;
using SourceCode.Workflow.Management;

namespace SourceCode.Tools.Management.Forms
{
    public partial class ProcessSetForm : Form
    {
        private readonly ProcessSet _procSet;

        public ProcessSetForm(ProcessSet procSet)
        {
            InitializeComponent();

            _procSet = procSet;

            this.Tag = _procSet;
            this.Text = _procSet.Name;
        }

        public ProcessSet ProcSet
        {
            get
            {
                return _procSet;
            }
        }

        internal void RefreshInstances()
        {
            instanceDataGridView.SelectionChanged -= instanceDataGridView_SelectionChanged;
            try
            {
                var instance = GetSelectedProcessInstance();
                var instances = K2Client.Instance.WorkflowManager.GetProcessInstances(this.ProcSet.ProcSetID);

                var bindingSource = new BindingSource();
                bindingSource.DataSource = DataTableCreator.GetDataTable(instances);

                instanceBindingNavigator.BindingSource = bindingSource;
                instanceDataGridView.DataSource = bindingSource;

                instanceDataGridView.Tag = instances;

                if (instance != null)
                {
                    var rowToSelect = (from r in instanceDataGridView.Rows.Cast<DataGridViewRow>()
                                       where Convert.ToInt32(r.Cells[Constants.DataTable.Instance.Id].Value).Equals(instance.ID)
                                       select r).FirstOrDefault();

                    if (rowToSelect != null)
                    {
                        rowToSelect.Selected = true;
                    }
                }

                ProcessInstanceSelected();
            }
            finally
            {
                instanceDataGridView.SelectionChanged += instanceDataGridView_SelectionChanged;
            }
        }

        private Activity GetSelectedProcessActivity()
        {
            var activities = activitiesDataGridView.Tag as Activity[];
            if (activities == null ||
                activitiesDataGridView.SelectedRows.Count == 0)
            {
                return null;
            }

            var activityName = Convert.ToString(activitiesDataGridView.SelectedRows[0].Cells[Constants.DataTable.Activity.Name].Value);

            var activity = activities.FirstOrDefault(i => i.Name == activityName);
            return activity;
        }

        private ProcessInstance GetSelectedProcessInstance()
        {
            var instances = instanceDataGridView.Tag as ProcessInstance[];
            if (instances == null ||
                instanceDataGridView.SelectedRows.Count == 0)
            {
                return null;
            }
            var processInstanceId = Convert.ToInt32(instanceDataGridView.SelectedRows[0].Cells[Constants.DataTable.Instance.Id].Value);

            var instance = instances.FirstOrDefault(i => i.ID == processInstanceId);
            return instance;
        }

        private void instanceDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!instanceDataGridView.Focused)
            {
                return;
            }

            ProcessInstanceSelected();
        }

        private void instanceDeleteToolStripButton_Click(object sender, EventArgs e)
        {
            var instance = GetSelectedProcessInstance();
            instance.ThrowIfNull(nameof(instance));

            if (MessageBoxHelper.DisplayQuestion(
              $"{Resources.DeleteProcessInstance} {instance.GetProcessInstanceDisplayName()}"
              , Resources.DeleteProcessInstance) != DialogResult.Yes)
            {
                return;
            }

            K2Client.Instance.WorkflowManager.DeleteProcessInstance(instance);

            this.RefreshInstances();
        }

        private void instanceGotoActivityToolStripButton_Click(object sender, EventArgs e)
        {
            var instance = GetSelectedProcessInstance();
            if (instance == null)
            {
                return;
            }

            var activity = GetSelectedProcessActivity();
            if (activity == null)
            {
                return;
            }

            if (MessageBoxHelper.DisplayQuestion(
             $"{Resources.GoToActivity} [{activity.Name}]?"
             , Resources.GoToActivity) != DialogResult.Yes)
            {
                return;
            }

            K2Client.Instance.WorkflowManager.GotoActivity(instance.ID, activity.Name);

            RefreshInstances();
        }

        private void instanceRefreshToolStripButton_Click(object sender, EventArgs e)
        {
            RefreshInstances();
        }

        private void instanceRestartToolStripButton_Click(object sender, EventArgs e)
        {
            var instance = GetSelectedProcessInstance();
            instance.ThrowIfNull(nameof(instance));

            if (MessageBoxHelper.DisplayQuestion(
                $"{Resources.RestartProcessInstance} {instance.GetProcessInstanceDisplayName()}"
                , Resources.RestartProcessInstance) != DialogResult.Yes)
            {
                return;
            }

            K2Client.Instance.WorkflowManager.StartProcessInstance(instance);

            this.RefreshInstances();
        }

        private void instanceRetryToolStripButton_Click(object sender, EventArgs e)
        {
            var instance = GetSelectedProcessInstance();
            instance.ThrowIfNull(nameof(instance));

            if (MessageBoxHelper.DisplayQuestion(
             $"{Resources.RetryProcessInstance} {instance.GetProcessInstanceDisplayName()}"
             , Resources.RetryProcessInstance) != DialogResult.Yes)
            {
                return;
            }

            K2Client.Instance.WorkflowManager.RetryProcessInstance(instance);

            this.RefreshInstances();
        }

        private void instanceStartNewToolStripButton_Click(object sender, EventArgs e)
        {
            using (var startNewProcess = new StartNewProcessForm(_procSet.ProcSetID))
            {
                startNewProcess.ShowDialog();
            }

            RefreshInstances();
        }

        private void instanceStopToolStripButton_Click(object sender, EventArgs e)
        {
            var instance = GetSelectedProcessInstance();
            instance.ThrowIfNull(nameof(instance));

            if (MessageBoxHelper.DisplayQuestion(
               $"{Resources.StopProcessInstance} {instance.GetProcessInstanceDisplayName()}"
               , Resources.StopProcessInstance) != DialogResult.Yes)
            {
                return;
            }

            K2Client.Instance.WorkflowManager.StopProcessInstance(instance);

            this.RefreshInstances();
        }

        private void ProcessInstanceSelected()
        {
            if (instanceDataGridView.SelectedRows.Count != 1)
            {
                return;
            }

            var instance = GetSelectedProcessInstance();

            UpdateProcessInstanceButtons(instance);

            RefreshActivities();
            RefreshViewFlow();
        }
        private void RefreshActivities()
        {
            var instance = GetSelectedProcessInstance();
            var activities = K2Client.Instance.WorkflowManager.GetProcActivities(instance?.ProcID);

            var bindingSource = new BindingSource();
            bindingSource.DataSource = DataTableCreator.GetDataTable(activities);

            activitiesBindingNavigator.BindingSource = bindingSource;
            activitiesDataGridView.DataSource = bindingSource;

            activitiesDataGridView.Tag = activities;
        }

        private void RefreshViewFlow()
        {
            var instance = GetSelectedProcessInstance();
            var url = K2Client.Instance.EnvironmentManager.GetViewflowUrl(instance.ID);
            if (!string.IsNullOrWhiteSpace(url))
            {
                viewFlowWebBrowser.Navigate(url);
            }
        }
        private void UpdateProcessInstanceButtons(ProcessInstance instance)
        {
            instanceRestartToolStripButton.Enabled = false;
            instanceStopToolStripButton.Enabled = false;
            instanceDeleteToolStripButton.Enabled = false;
            instanceViewFlowToolStripButton.Enabled = false;
            instanceRetryToolStripButton.Enabled = false;
            instanceGotoActivityToolStripButton.Enabled = false;

            if (instance == null)
            {
                return;
            }

            switch (instance.GetProcessInstanceStatus())
            {
                case ProcessInstanceStatus.Error:
                    instanceRetryToolStripButton.Enabled = true;
                    break;

                case ProcessInstanceStatus.Running:
                    instanceStopToolStripButton.Enabled = true;
                    break;

                case ProcessInstanceStatus.Active:
                    instanceStopToolStripButton.Enabled = true;
                    instanceDeleteToolStripButton.Enabled = true;
                    instanceGotoActivityToolStripButton.Enabled = true;
                    instanceViewFlowToolStripButton.Enabled = true;
                    break;

                case ProcessInstanceStatus.Completed:
                    break;

                case ProcessInstanceStatus.Stopped:
                    instanceRestartToolStripButton.Enabled = true;
                    instanceDeleteToolStripButton.Enabled = true;
                    instanceViewFlowToolStripButton.Enabled = true;
                    break;

                case ProcessInstanceStatus.Deleted:
                    break;

                case ProcessInstanceStatus.Undefined:
                    break;

                default:
                    break;
            }
        }
    }
}