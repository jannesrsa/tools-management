using System.Collections.Generic;
using System.Linq;
using SourceCode.EnvironmentSettings.Client;

namespace Tools.Management.DataAccessLayer.Managers
{
    internal class EnvironmentManager : K2ManagerBase
    {
        public Dictionary<string, string> GetDefaultEnvironmentFields()
        {
            var server = this.GetEnvironmentSettingsManager();
            using (server)
            {
                var template = server.EnvironmentTemplates.DefaultTemplate;
                return template.DefaultEnvironment.EnvironmentFields
                    .Cast<EnvironmentField>()
                    .ToDictionary(i => i.FieldName, i => i.Value);
            }
        }

        public string GetViewflowUrl(int processId)
        {
            var environmentFields = this.GetDefaultEnvironmentFields();

            string value;
            if (environmentFields.TryGetValue(Constants.EnvironmentField.K2ViewflowUrl, out value) ||
                environmentFields.TryGetValue(Constants.EnvironmentField.WebServiceUrlSsl, out value) ||
                environmentFields.TryGetValue(Constants.EnvironmentField.WebServiceUrl, out value))
            {
                return $"{value}{Constants.Url.ViewFlow}{processId}";
            }

            return null;
        }

        internal EnvironmentSettingsManager GetEnvironmentSettingsManager()
        {
            var environmentSettingsManager = new EnvironmentSettingsManager(false, false);

            environmentSettingsManager.ConnectToServer(_k2Client.K2Config.HostServerConnectionString);
            environmentSettingsManager.InitializeSettingsManager(true);

            return environmentSettingsManager;
        }
    }
}