using System.Collections.Generic;
using SourceCode.Tools.Management.DataAccessLayer.Managers;

namespace SourceCode.Tools.Management.DataAccessLayer
{
    /// <summary>
    /// Contains all the K2 API managers
    /// </summary>
    internal sealed class K2Client
    {
        private static readonly K2Client _instance = new K2Client();
        private readonly K2Config _k2Config = new K2Config();
        private List<K2ManagerBase> _managers = new List<K2ManagerBase>();

        private K2Client()
        {
            InitManagers();
        }

        public static K2Client Instance
        {
            get
            {
                return _instance;
            }
        }

        public EnvironmentManager EnvironmentManager { get; private set; }

        public SmartObjectManager SmartObjectManager { get; private set; }

        public WorkflowClient WorkflowClient { get; private set; }

        public WorkflowManager WorkflowManager { get; private set; }

        internal K2Config K2Config
        {
            get
            {
                return _k2Config;
            }
        }

        public void SetDefaultHostServerConnectionString()
        {
            this.K2Config.HostServerConnectionString = K2Config.GetDefaultHostServerConnectionString();
        }

        private void InitManagers()
        {
            // Init Resource Managers
            WorkflowManager = new WorkflowManager();
            _managers.Add(WorkflowManager);

            SmartObjectManager = new SmartObjectManager();
            _managers.Add(SmartObjectManager);

            WorkflowClient = new WorkflowClient();
            _managers.Add(WorkflowClient);

            EnvironmentManager = new EnvironmentManager();
            _managers.Add(EnvironmentManager);

            _managers.ForEach(i => i.SetK2Client(this));
        }
    }
}