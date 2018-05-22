using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SourceCode.Hosting.Client.BaseAPI;
using Tools.Management.DataAccessLayer;
using Tools.Management.Extensions;
using Tools.Management.Model;
using SourceCode.Workflow.Management;

namespace Tools.Management
{
    public partial class ServerPropsForm : Form
    {
        private bool _changed = true;
        private Dictionary<string, SCConnectionStringBuilder> _servers = new Dictionary<string, SCConnectionStringBuilder>();

        public ServerPropsForm()
        {
            InitializeComponent();
            ConnectionBuilder = new SCConnectionStringBuilder();
        }

        public SCConnectionStringBuilder ConnectionBuilder
        {
            get;
            private set;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cboRecentServers.SelectedItem is DisplayServer)
            {
                SCConnectionStringBuilder serverSetting = (cboRecentServers.SelectedItem as DisplayServer).ServerSetting;
                var config = new ServerConfiguration();
                config.Remove(serverSetting);

                cboRecentServers.Items.Remove(cboRecentServers.SelectedItem);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_changed)
            {
                if (!CheckConnection(false))
                {
                    return;
                }
            }

            ServerConfiguration config = new ServerConfiguration();
            config.CreateNewServerConfig(ConnectionBuilder);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            CheckConnection(false);
            _changed = false;
        }

        private void cboRecentServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SCConnectionStringBuilder selectedServer = ((sender as ComboBox).SelectedItem as DisplayServer).ServerSetting;
            ConnectionBuilder = selectedServer;
            this.UpdateTextBoxes();
            btnDelete.Enabled = true;
        }

        private bool CheckConnection(bool display)
        {
            bool result = false;

            var workflowManagementServer = new WorkflowManagementServer();

            try
            {
                if (string.IsNullOrEmpty(txtServerName.Text) ||
                    string.IsNullOrEmpty(txtServerPort.Text.ToString()))
                {
                    this.DisplayError("Enter servername and port", "ServerName or Port empty");
                    return result;
                }

                ConnectionBuilder = new SCConnectionStringBuilder();
                ConnectionBuilder.Host = txtServerName.Text;
                ConnectionBuilder.Port = uint.Parse(txtServerPort.Text);
                ConnectionBuilder.IsPrimaryLogin = true;
                ConnectionBuilder.Integrated = chkIntegratedSecurity.Checked;

                if (!string.IsNullOrEmpty(txtUserName.Text))
                {
                    if (string.IsNullOrEmpty(txtServerName.Text) ||
                    string.IsNullOrEmpty(txtServerPort.Text.ToString()))
                    {
                        this.DisplayError("Enter servername and port", "ServerName or Port empty");
                        return result;
                    }
                    ConnectionBuilder.UserID = txtUserName.Text;
                    ConnectionBuilder.Password = txtUserPassword.Text;
                    ConnectionBuilder.SecurityLabelName = txtLabel.Text;
                    ConnectionBuilder.WindowsDomain = txtDomain.Text;
                }

                workflowManagementServer.CreateConnection();
                workflowManagementServer.Connection.Open(ConnectionBuilder.ConnectionString);

                if (display)
                {
                    this.DisplayInfo("Connection to '" + txtServerName.Text + "' succeeded",
                        "Connection succeeded");
                }
                btnOK.Enabled = true;
                result = true;
            }
            catch (Exception ex)
            {
                this.DisplayError("Connection Failed to '" + txtServerName.Text + ":" + txtServerPort.Text + "' Message: " + ex.Message, "Connection Failed");
                btnOK.Enabled = false;
                result = false;
            }
            finally
            {
                this.Stop();
                workflowManagementServer?.Connection?.Dispose();
            }

            return result;
        }

        private void ServerPropsForm_Load(object sender, EventArgs e)
        {
            this.UpdateTextBoxes();
            this.UpdateServers();
        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {
            _changed = true;
            btnOK.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void txtServerPort_TextChanged(object sender, EventArgs e)
        {
            _changed = true;
            btnOK.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void UpdateServers()
        {
            foreach (SCConnectionStringBuilder serverSettting in ServerConfiguration.GetAllServerConfiguration())
            {
                var serverName = serverSettting.GetDisplayName();
                var displayServer = new DisplayServer(serverName);
                displayServer.ServerSetting = serverSettting;

                cboRecentServers.Items.Add(displayServer);
                _servers.Add(serverName, serverSettting);
            }

            if (cboRecentServers.Items.Count > 0)
            {
                cboRecentServers.SelectedIndex = 0;
                cboRecentServers.Text = cboRecentServers.SelectedText;
            }
        }

        private void UpdateTextBoxes()
        {
            if (string.IsNullOrEmpty(ConnectionBuilder.Host))
            {
                ConnectionBuilder.ConnectionString = K2Client.Instance.K2Config.HostServerConnectionString;
            }

            chkIntegratedSecurity.Checked = ConnectionBuilder.Integrated;
            txtServerName.Text = ConnectionBuilder.Host;
            txtServerPort.Text = ConnectionBuilder.Port.ToString();

            txtLabel.Text = ConnectionBuilder.SecurityLabelName;
            txtUserName.Text = ConnectionBuilder.UserID;
            txtUserPassword.Text = ConnectionBuilder.Password;
            txtDomain.Text = ConnectionBuilder.WindowsDomain;
        }

        private class DisplayServer
        {
            public DisplayServer(string text)
            {
                this.Text = text;
            }

            public SCConnectionStringBuilder ServerSetting { get; set; }

            public string Text { get; set; }

            public override int GetHashCode()
            {
                return this.Text.GetHashCode();
            }

            public override string ToString()
            {
                return this.Text;
            }
        }
    }
}