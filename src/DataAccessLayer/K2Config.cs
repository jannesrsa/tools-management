using System;
using SourceCode.EnvironmentSettings.Client;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Workflow.Design.EnvironmentSettings;

namespace SourceCode.Tools.Management.DataAccessLayer
{
    /// <summary>
    /// Contains the K2 Configuration settings
    /// </summary>
    internal class K2Config
    {
        public string HostServerConnectionString { get; set; }

        public string WorkflowClientConnectionString
        {
            get
            {
                var workflowConnectionStringBuilder = new SCConnectionStringBuilder(this.HostServerConnectionString);
                workflowConnectionStringBuilder.Port = 5252;
                return workflowConnectionStringBuilder.ConnectionString;
            }
        }

        public static string GetDefaultHostServerConnectionString()
        {
            var builder = new SCConnectionStringBuilder();
            builder.Host = Environment.MachineName;
            builder.Port = 5555;
            builder.IsPrimaryLogin = true;
            builder.Integrated = true;

            var connectionString = builder.ConnectionString;

            try
            {
                using (var environmentSettingsManager = new EnvironmentSettingsManager(false))
                {
                    environmentSettingsManager.ConnectionString = connectionString;
                    environmentSettingsManager.InitializeSettingsManager(false);

                    if (environmentSettingsManager.EnvironmentTemplates.Count <= 0)
                    {
                        environmentSettingsManager.ConnectToServer();
                        environmentSettingsManager.InitializeSettingsManager(true);
                        environmentSettingsManager.Disconnect();
                    }

                    if (environmentSettingsManager.CurrentEnvironment != null)
                    {
                        var smartObjectfield = (SmartObjectField)environmentSettingsManager.CurrentEnvironment.GetDefaultField(typeof(SmartObjectField));

                        if (smartObjectfield != null)
                        {
                            return smartObjectfield.ConnectionString;
                        }
                    }
                }
            }
            catch { }

            return connectionString;
        }
    }
}