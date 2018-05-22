using System.Linq;
using SourceCode.SmartObjects.Authoring;
using SourceCode.SmartObjects.Management;
using SourceCode.Workflow.Management;

namespace SourceCode.Tools.Management.DataAccessLayer.Managers
{
    /// <summary>
    /// Handles all SmartObject API calls
    /// </summary>
    /// <seealso cref="SourceCode.Tools.Management.DataAccessLayer.Managers.K2ManagerBaseOfType{SourceCode.SmartObjects.Management.SmartObjectManagementServer}" />
    internal class SmartObjectManager : K2ManagerBaseOfType<SmartObjectManagementServer>
    {
        public void DeleteWorkflowReportingSmartObjects(ProcessSet processSet)
        {
            var smartObjectManagementServer = this.GetServer();
            using (smartObjectManagementServer.Connection)
            {
                var serviceObjectName = $"Rpt_{SmartObjectDefinition.GetNameFromDisplay(processSet.FullName)}";

                var smartObjects = from smo in smartObjectManagementServer.GetSmartObjects(Constants.ServiceInstance.WorkflowReporting).SmartObjectList
                                   from se in smo.Metadata.ServiceElements.Cast<ServiceElementInfo>()
                                   where se.Name == "serviceobject" && se.Value == serviceObjectName
                                   select smo;

                foreach (var smartobject in smartObjects)
                {
                    smartObjectManagementServer.DeleteSmartObject(smartobject.Guid, true);
                }
            }
        }
    }
}