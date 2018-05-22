using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Tools.Management.DataAccessLayer;
using Tools.Management.DataAccessLayer.Model;
using Tools.Management.Extensions;
using Tools.Management.Properties;
using SourceCode.Workflow.Management;

namespace Tools.Management.Forms
{
    public partial class StartNewProcessForm : Form
    {
        private readonly int _procSetId;
        private DataField[] _dataFields;
        private ProcessSet _processSet;

        public StartNewProcessForm(int procSetId)
        {
            InitializeComponent();

            _procSetId = procSetId;

            this.RefreshValues();
        }

        private void instanceRefreshToolStripButton_Click(object sender, EventArgs e)
        {
            RefreshValues();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            K2Client.Instance.WorkflowClient.StartNewProcessInstance(_processSet.ProcID, _dataFields, folioTextBox.Text, asynchronousCheckBox.Checked);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void RefreshValues()
        {
           Action action = () =>
            {
                folioTextBox.Text = DateTime.Now.ToString("s");

                _processSet = K2Client.Instance.WorkflowManager.GetProcessSet(_procSetId);
                _dataFields = K2Client.Instance.WorkflowManager.GetDataFields(_processSet.ProcID);

                dataFieldsScrollableControl.Controls.Clear();

                var panelColor = this.BackColor;

                foreach (var dataField in _dataFields)
                {
                    var dataFieldPanel = new Panel();
                    dataFieldPanel.Tag = dataField;

                    var dataFieldLabel = new Label();
                    dataFieldLabel.AutoSize = true;
                    dataFieldLabel.Text = dataField.Name;

                    if (dataField.FieldType == DataField.ProcessXMLField)
                    {
                        dataFieldLabel.Text += $" ( {dataField.FieldType} ) ";
                    }

                    dataFieldLabel.Dock = DockStyle.Left;
                    dataFieldPanel.Controls.Add(dataFieldLabel);

                    var dataFieldTextBox = new TextBox();
                    dataFieldTextBox.Dock = DockStyle.Fill;

                    var textButton = new Button();
                    textButton.Text = Resources.Edit;
                    textButton.AutoSize = true;
                    textButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    textButton.Dock = DockStyle.Right;
                    textButton.BackColor = this.BackColor;

                    dataFieldPanel.Controls.Add(textButton);
                    textButton.Click += new EventHandler(textButton_Click);

                    dataFieldPanel.Controls.Add(dataFieldTextBox);
                    dataFieldPanel.Height = dataFieldTextBox.Height;
                    dataFieldPanel.BackColor = panelColor;

                    panelColor = (panelColor == this.BackColor) ? Color.Silver : this.BackColor;

                    dataFieldTextBox.BringToFront();
                    dataFieldTextBox.DataBindings.Add(new Binding("Text", dataField, "Value", false, DataSourceUpdateMode.OnPropertyChanged));

                    dataFieldsScrollableControl.Controls.Add(dataFieldPanel);
                    dataFieldPanel.Dock = DockStyle.Top;
                    dataFieldPanel.BringToFront();
                }

                var labels = (from c in dataFieldsScrollableControl.Controls.OfType<Panel>()
                              from l in c.Controls.OfType<Label>()
                              select l)
                             .EmptyIfNull()
                             .ToArray();

                var maxWidth = labels.Length == 0 ? 0 : labels.Max(i => i.Width);

                // Set the labels to have same width
                foreach (var label in labels)
                {
                    label.AutoSize = false;
                    label.Width = maxWidth;
                }
            };

            this.Draw(action);
        }

        private void textButton_Click(object sender, EventArgs e)
        {
            var textButton = (Button)sender;
            var textBox = textButton.Parent.Controls.OfType<TextBox>().Single();

            using (var textInputForm = new TextInputForm(textBox.Text))
            {
                if (textInputForm.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = textInputForm.TextRichTextBox.Text;
                }
            }
        }
    }
}