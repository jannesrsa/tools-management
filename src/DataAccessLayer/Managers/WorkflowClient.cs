using System.Linq;
using SourceCode.Tools.Management.DataAccessLayer.Model;
using SourceCode.Tools.Management.Extensions;

namespace SourceCode.Tools.Management.DataAccessLayer.Managers
{
    internal class WorkflowClient : K2ManagerBase
    {
        public string GetUserName()
        {
            string loggedInUserFqn;
            using (var connection = GetServer())
            {
                loggedInUserFqn = connection.User.FQN;
                loggedInUserFqn.ThrowIfNullOrWhiteSpace(nameof(loggedInUserFqn));
            }

            return loggedInUserFqn;
        }

        public int StartNewProcessInstance(int procId, DataField[] dataFields, string folio, bool sync)
        {
            var process = _k2Client.WorkflowManager.GetProcess(procId);

            var server = GetServer();
            using (server)
            {
                var processInstance = server.CreateProcessInstance(process.FullName, process.VersionNumber);

                processInstance.Folio = folio;

                // Set data fields
                foreach (SourceCode.Workflow.Client.DataField clientDataField in processInstance.DataFields)
                {
                    var dataField = dataFields.EmptyIfNull().FirstOrDefault(i => i.Name == clientDataField.Name);
                    if (dataField != null)
                    {
                        clientDataField.Value = dataField.Value;
                    }
                }

                // Set xml fields
                foreach (SourceCode.Workflow.Client.XmlField clientXmlField in processInstance.XmlFields)
                {
                    var xmlField = dataFields.EmptyIfNull().FirstOrDefault(i => i.Name == clientXmlField.Name);
                    if (xmlField != null)
                    {
                        clientXmlField.Value = xmlField.Value;
                    }
                }

                server.StartProcessInstance(processInstance, sync);
                return processInstance.ID;
            }
        }

        private SourceCode.Workflow.Client.Connection GetServer()
        {
            var connection = new SourceCode.Workflow.Client.Connection();
            var connectionSetup = new SourceCode.Workflow.Client.ConnectionSetup();
            connectionSetup.ParseConnectionString(_k2Client.K2Config.WorkflowClientConnectionString);

            connection.Open(connectionSetup);

            return connection;
        }
    }
}