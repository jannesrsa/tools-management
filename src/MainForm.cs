using System;
using System.Linq;
using System.Windows.Forms;
using SourceCode.Hosting.Client.BaseAPI;
using Tools.Management.DataAccessLayer;
using Tools.Management.Extensions;
using Tools.Management.Factories;
using Tools.Management.Forms;
using Tools.Management.Helpers;
using Tools.Management.Model;
using Tools.Management.Properties;
using SourceCode.Workflow.Management;

namespace Tools.Management
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.Initialize();
        }

        private void ChangeServerStatus()
        {
            var connectionStringBuilder = new SCConnectionStringBuilder(K2Client.Instance.K2Config.HostServerConnectionString);
            tssServer.Text = connectionStringBuilder.GetDisplayName();
        }

        private void connectionPropertiesToolStripButton_Click(object sender, EventArgs e)
        {
            using (var hostServer = new ServerPropsForm())
            {
                var dialogResult = hostServer.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    K2Client.Instance.K2Config.HostServerConnectionString = hostServer.ConnectionBuilder.ConnectionString;
                    this.SyncFromServer();
                }

                GetRecentServers();
                ChangeServerStatus();
            }
        }

        private void CreateTreeNodes(ProcessFolder[] processFolders, Process[] processes, ProcessInstance[] processInstances)
        {
            var workflowsTreeNode = mainTreeView.Nodes[0];
            workflowsTreeNode.Expand();
            workflowsTreeNode.Nodes.Clear();

            foreach (var processFolder in processFolders.OrderBy(i => i.FolderName))
            {
                // ProcessFolder
                var processFolderTreeNode = TreeNodeCreator.Create(processFolder);
                workflowsTreeNode.Nodes.Add(processFolderTreeNode);
                processFolderTreeNode.ContextMenuStrip = processFolderContextMenuStrip;

                foreach (var procSet in processFolder.ProcessSets.Cast<ProcessSet>().OrderBy(i => i.Name))
                {
                    // ProcSet
                    var procSetTreeNode = TreeNodeCreator.Create(procSet);
                    procSetTreeNode.ContextMenuStrip = procSetContextMenuStrip;
                    processFolderTreeNode.Nodes.Add(procSetTreeNode);

                    var procSetProcesses = processes.Where(i => i.ProcSetID == procSet.ProcSetID);

                    TreeNode versionsFolder = null;
                    foreach (var process in procSetProcesses.OrderByDescending(i => i.VersionNumber))
                    {
                        // Process
                        var processTreeNode = TreeNodeCreator.Create(process);

                        if (process.DefaultVersion)
                        {
                            procSetTreeNode.Nodes.Add(processTreeNode);
                        }
                        else
                        {
                            if (versionsFolder == null)
                            {
                                versionsFolder = TreeNodeCreator.CreateVersionsFolder();
                                procSetTreeNode.Nodes.Insert(0, versionsFolder);
                            }

                            versionsFolder.Nodes.Add(processTreeNode);
                        }

                        // ProcessInstance
                        var processProcessInstances = processInstances.Where(i => i.ProcID == process.ProcID);

                        foreach (var processInstance in processProcessInstances.OrderBy(i => i.StartDate))
                        {
                            var processInstanceTreeNode = TreeNodeCreator.Create(processInstance);
                            processTreeNode.Nodes.Add(processInstanceTreeNode);
                        }
                    }
                }
            }
        }

        private void deleteProcessFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var processFolder = GetSelectedNode<ProcessFolder>();
            if (processFolder == null)
            {
                return;
            }

            if (MessageBoxHelper.DisplayQuestion(
                $"{Resources.DeleteProcessFolder} [{processFolder.FolderName}]?"
                , Resources.Delete) != DialogResult.Yes)
            {
                return;
            }

            this.Busy();
            try
            {
                foreach (ProcessSet processSet in processFolder.ProcessSets)
                {
                    K2Client.Instance.WorkflowManager.DeleteProcessSet(processSet.ProcSetID);
                }

                this.SyncFromServer();
            }
            finally
            {
                this.Stop();
            }
        }

        private void deleteProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var processSet = GetSelectedNode<ProcessSet>();
            if (processSet == null)
            {
                return;
            }

            if (MessageBoxHelper.DisplayQuestion(
                $"{Resources.DeleteProcess} [{processSet.Name}]?"
                , Resources.Delete) != DialogResult.Yes)
            {
                return;
            }

            this.Busy();
            try
            {
                K2Client.Instance.WorkflowManager.DeleteProcessSet(processSet.ProcSetID);

                var processSetForm = this.MdiChildren.Cast<Form>()
                    .FirstOrDefault(i => i.Tag == processSet);

                if (processSetForm != null)
                {
                    processSetForm.Close();
                }

                this.SyncFromServer();
            }
            finally
            {
                this.Stop();
            }
        }

        private void deleteWorkflowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBoxHelper.DisplayQuestion(Resources.DeleteWorkflows, Resources.Delete) != DialogResult.Yes)
            {
                return;
            }

            this.Busy();
            try
            {
                foreach (ProcessSet processSet in K2Client.Instance.WorkflowManager.GetProcessFolders().SelectMany(i => i.ProcessSets.Cast<ProcessSet>()))
                {
                    K2Client.Instance.WorkflowManager.DeleteProcessSet(processSet.ProcSetID);
                }

                this.SyncFromServer();
            }
            finally
            {
                this.Stop();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetConnection()
        {
            var configuration = new ServerConfiguration();
            var lastConnectionString = configuration.GetCurrentServerConfiguration();

            if (!string.IsNullOrEmpty(lastConnectionString))
            {
                K2Client.Instance.K2Config.HostServerConnectionString = lastConnectionString;
            }
            else
            {
                K2Client.Instance.SetDefaultHostServerConnectionString();
            }

            ChangeServerStatus();
            GetRecentServers();
        }

        private void GetRecentServers()
        {
            tsmRecentServers.DropDownItems.Clear();
            tssServer.DropDownItems.Clear();

            tssServer.Alignment = ToolStripItemAlignment.Right;

            foreach (SCConnectionStringBuilder serverSettting in ServerConfiguration.GetAllServerConfiguration())
            {
                ToolStripMenuItem item = new ToolStripMenuItem(serverSettting.GetDisplayName());
                item.Tag = serverSettting;
                tsmRecentServers.DropDownItems.Add(item);

                ToolStripMenuItem item2 = new ToolStripMenuItem(serverSettting.GetDisplayName());
                item2.Tag = serverSettting;
                tssServer.DropDownItems.Add(item2);

                item2.Click += new EventHandler(recentServer_Click);
                item.Click += new EventHandler(recentServer_Click);
            }
        }

        private T GetSelectedNode<T>()
            where T : class
        {
            return mainTreeView?.SelectedNode?.Tag as T;
        }

        private void Initialize()
        {
            try
            {
                GetConnection();

                // Initialize SourceCode ConnectionString
                if (string.IsNullOrEmpty(K2Client.Instance.K2Config.HostServerConnectionString))
                {
                    K2Client.Instance.SetDefaultHostServerConnectionString();
                }

                LoadSettings();

                this.SyncFromServer();
            }
            catch (Exception ex)
            {
                this.DisplayError(ex);
            }
        }

        private void LoadSettings()
        {
            if (Settings.Default.Options != null)
            {
                if (!Settings.Default.Options.Location.IsEmpty)
                {
                    this.Location = Settings.Default.Options.Location;
                }

                if (!Settings.Default.Options.Size.IsEmpty)
                {
                    this.Size = Settings.Default.Options.Size;
                    this.WindowState = Settings.Default.Options.WindowState;
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            var processSetForm = this.ActiveMdiChild as ProcessSetForm;
            if (processSetForm != null)
            {
                processSetForm.RefreshInstances();
            }
        }

        private void mainTreeView_MouseDown(object sender, MouseEventArgs e)
        {
            this.mainTreeView.SelectedNode = this.mainTreeView.GetNodeAt(e.X, e.Y);
        }

        private void mainTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null ||
                e.Node.Tag == null)
            {
                return;
            }

            this.StartRedraw();

            try
            {
                var processSet = e.Node.Tag as ProcessSet;
                if (processSet != null &&
                    e.Button == MouseButtons.Left)
                {
                    var processSetForm = this.MdiChildren.OfType<ProcessSetForm>()
                        .FirstOrDefault(i => i.Tag == processSet);

                    if (processSetForm == null)
                    {
                        processSetForm = new ProcessSetForm(processSet);
                        processSetForm.MdiParent = this;
                        processSetForm.Tag = processSet;
                    }

                    processSetForm.WindowState = FormWindowState.Maximized;
                    processSetForm.BringToFront();
                    processSetForm.Select();
                    processSetForm.Show();
                }
            }
            finally
            {
                this.StopRedraw();
            }
        }

        private void recentServer_Click(object sender, EventArgs e)
        {
            SCConnectionStringBuilder selectedServer = (SCConnectionStringBuilder)(sender as ToolStripMenuItem).Tag;

            ServerConfiguration config = new ServerConfiguration();
            config.CreateNewServerConfig(selectedServer);

            this.GetRecentServers();
            K2Client.Instance.K2Config.HostServerConnectionString = selectedServer.ConnectionString;
            this.ChangeServerStatus();

            SyncFromServer();
        }

        private void SaveSettings()
        {
            if (Settings.Default.Options == null)
            {
                Settings.Default.Options = new Options();
            }

            Settings.Default.Options.Location = this.Location;
            Settings.Default.Options.Size = this.Size;
            Settings.Default.Options.WindowState = this.WindowState;

            Settings.Default.Save();
        }

        private void SyncFromServer()
        {
            this.Busy();
            mainTreeView.BeginUpdate();

            var treeViewState = mainTreeView.GetTreeViewTreeState();

            try
            {
                var processFolders = K2Client.Instance.WorkflowManager.GetProcessFolders();
                var processes = K2Client.Instance.WorkflowManager.GetProcesses();
                var processInstances = K2Client.Instance.WorkflowManager.GetProcessInstances();

                CreateTreeNodes(processFolders, processes, processInstances);
                UpdateForms(processFolders);

                mainTreeView.LoadVisibleExpandedTreeState(treeViewState.Item2, treeViewState.Item1);
            }
            catch (Exception ex)
            {
                mainTreeView.EndUpdate();
                this.Stop();
                MessageBoxHelper.DisplayError(ex);
            }
            finally
            {
                mainTreeView.EndUpdate();
                this.Stop();
            }
        }

        private void syncFromServerToolStripButton_Click(object sender, System.EventArgs e)
        {
            SyncFromServer();
        }

        private void testCategoryRichTextBox_TextChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void UpdateForms(ProcessFolder[] processFolders)
        {
            var processSets = (from f in processFolders
                               from p in f.ProcessSets.Cast<ProcessSet>()
                               select p)
                               .ToArray();

            foreach (var form in this.MdiChildren.ToArray())
            {
                var formProcess = form.Tag as ProcessSet;
                if (formProcess != null)
                {
                    var processSet = processSets.FirstOrDefault(i => i.ProcSetID == formProcess.ProcSetID);
                    if (processSet != null)
                    {
                        form.Tag = processSet;
                    }
                    else
                    {
                        form.Close();
                    }
                }
            }
        }
    }
}