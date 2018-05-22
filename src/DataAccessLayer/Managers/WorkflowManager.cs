using System.Linq;
using Tools.Management.DataAccessLayer.Model;
using Tools.Management.Extensions;
using SourceCode.Workflow.Management;
using SourceCode.Workflow.Management.Criteria;

namespace Tools.Management.DataAccessLayer.Managers
{
    /// <summary>
    /// Handles all Workflow API calls
    /// </summary>
    /// <seealso cref="Tools.Management.DataAccessLayer.Managers.K2ManagerBaseOfType{SourceCode.Workflow.Management.WorkflowManagementServer}" />
    internal class WorkflowManager : K2ManagerBaseOfType<WorkflowManagementServer>
    {
        public void DeleteProcessInstance(ProcessInstance processInstance)
        {
            processInstance.ThrowIfNull(nameof(processInstance));

            var workflowManagementServer = this.GetServer();
            using (workflowManagementServer.Connection)
            {
                workflowManagementServer.DeleteProcessInstances(processInstance.ID, true);
            }
        }

        public void DeleteProcessSet(int procSetId)
        {
            var processSet = this.GetProcessSet(procSetId);
            processSet.ThrowIfNull(nameof(processSet));

            var workflowManagementServer = this.GetServer();
            using (workflowManagementServer.Connection)
            {
                K2Client.Instance.SmartObjectManager.DeleteWorkflowReportingSmartObjects(processSet);
                workflowManagementServer.DeleteProcessDefinition(processSet.FullName, 0, true);
            }
        }

        public DataField[] GetDataFields(int? procID)
        {
            if (!procID.HasValue)
            {
                return Enumerable.Empty<DataField>().ToArray();
            }

            var server = this.GetServer();
            using (server.Connection)
            {
                var processDataFields = server.GetProcessDataFields(procID.Value);
                var xmlFields = server.GetProcessXMLFields(procID.Value);

                return DataField.GetEnumerable(processDataFields.Cast<SourceCode.Workflow.Management.ProcessDataField>())
                        .Union(DataField.GetEnumerable(xmlFields.Cast<SourceCode.Workflow.Management.ProcessXMLField>()))
                        .ToArray();
            }
        }

        public ErrorLog GetErrorLog(int procId, int procInstId, int errorProfileId)
        {
            var server = this.GetServer();
            using (server.Connection)
            {
                var criteria = new ErrorLogCriteriaFilter();
                criteria.PageSize = int.MaxValue;
                criteria.PageIndex = 0;
                criteria.AddRegularFilter(ErrorLogFields.ProcInstID, Comparison.Equals, procInstId);

                var errorLogs = server.GetErrorLogs(errorProfileId, criteria).Cast<ErrorLog>();
                return errorLogs.FirstOrDefault(l => l.ProcID == procId);
            }
        }

        public Activity[] GetProcActivities(int? procID)
        {
            if (!procID.HasValue)
            {
                return Enumerable.Empty<Activity>().ToArray();
            }

            var workflowManagementServer = GetServer();
            using (workflowManagementServer.Connection)
            {
                return workflowManagementServer.GetProcActivities(procID.Value).Cast<Activity>().ToArray();
            }
        }

        public Process[] GetProcesses()
        {
            var workflowManagementServer = this.GetServer();
            using (workflowManagementServer.Connection)
            {
                return workflowManagementServer.GetProcesses(new ProcessCriteriaFilter()).Cast<Process>().ToArray();
            }
        }

        public ProcessFolder[] GetProcessFolders()
        {
            var workflowManagementServer = this.GetServer();
            using (workflowManagementServer.Connection)
            {
                return workflowManagementServer.GetProcessFolders().Cast<ProcessFolder>().ToArray();
            }
        }

        public ProcessInstance[] GetProcessInstances(int? procSetId)
        {
            if (!procSetId.HasValue)
            {
                return Enumerable.Empty<ProcessInstance>().ToArray();
            }

            var workflowManagementServer = this.GetServer();
            using (workflowManagementServer.Connection)
            {
                var processInstanceCriteriaFilter = new ProcessInstanceCriteriaFilter();
                processInstanceCriteriaFilter.AddRegularFilter(ProcessInstanceFields.ProcSetID, Comparison.Equals, procSetId);

                return workflowManagementServer.GetProcessInstancesAll(processInstanceCriteriaFilter).Cast<ProcessInstance>().ToArray();
            }
        }

        public ProcessInstance[] GetProcessInstances()
        {
            var workflowManagementServer = this.GetServer();
            using (workflowManagementServer.Connection)
            {
                return workflowManagementServer.GetProcessInstancesAll(new ProcessInstanceCriteriaFilter()).Cast<ProcessInstance>().ToArray();
            }
        }

        public bool GotoActivity(int procInstID, string activityName)
        {
            var server = GetServer();
            using (server.Connection)
            {
                return server.GotoActivity(procInstID, activityName);
            }
        }

        public bool RetryError(ErrorLog errorLog)
        {
            var workflowManagementServer = this.GetServer();
            using (workflowManagementServer.Connection)
            {
                var errorLogContext = (ErrorLogContext)errorLog.TypeID;
                switch (errorLogContext)
                {
                    case ErrorLogContext.IPCReturn:
                        SourceCode.Workflow.Management.IPCReturnErrorDetails ipcReturnErrorDetails = workflowManagementServer.GetIPCReturnErrorDetails(errorLog.ObjectID);
                        if (ipcReturnErrorDetails != null)
                        {
                            return workflowManagementServer.RepairIPCReturnError(errorLog.ID, ipcReturnErrorDetails.Server, ipcReturnErrorDetails.Port);
                        }
                        else
                        {
                            return workflowManagementServer.DeleteError(errorLog.ProcInstID, errorLog.ID);
                        }
                    case ErrorLogContext.IPCSend:
                    case ErrorLogContext.IPCFetch:
                    case ErrorLogContext.IPCDelete:
                        SourceCode.Workflow.Management.IPCErrorDetails ipcErrorDetails = workflowManagementServer.GetIPCErrorDetails(errorLog.ObjectID);
                        if (ipcErrorDetails != null)
                        {
                            return workflowManagementServer.RepairIPCError(errorLog.ID, ipcErrorDetails.Server, ipcErrorDetails.Port, ipcErrorDetails.ConnectionString, ipcErrorDetails.Process);
                        }
                        else
                        {
                            return workflowManagementServer.DeleteError(errorLog.ProcInstID, errorLog.ID);
                        }
                    default:
                        //API will check for process admin permissions in this call.
                        return workflowManagementServer.RetryError(errorLog.ProcInstID, errorLog.ID, K2Client.Instance.WorkflowClient.GetUserName());
                }
            }
        }

        public void StartProcessInstance(ProcessInstance processInstance)
        {
            processInstance.ThrowIfNull(nameof(processInstance));

            var workflowManagementServer = this.GetServer();
            using (workflowManagementServer.Connection)
            {
                workflowManagementServer.StartProcessInstances(processInstance.ID);
            }
        }

        public void StopProcessInstance(ProcessInstance processInstance)
        {
            processInstance.ThrowIfNull(nameof(processInstance));

            var workflowManagementServer = this.GetServer();
            using (workflowManagementServer.Connection)
            {
                workflowManagementServer.StopProcessInstances(processInstance.ID);
            }
        }

        internal Process GetProcess(int? procId)
        {
            if (!procId.HasValue)
            {
                return null;
            }

            var workflowManagementServer = this.GetServer();
            using (workflowManagementServer.Connection)
            {
                return workflowManagementServer.GetProcess(procId.Value);
            }
        }

        internal ProcessSet GetProcessSet(int? procSetId)
        {
            if (!procSetId.HasValue)
            {
                return null;
            }

            var workflowManagementServer = this.GetServer();
            using (workflowManagementServer.Connection)
            {
                return workflowManagementServer.GetProcSet(procSetId.Value);
            }
        }

        internal void RetryProcessInstance(ProcessInstance instance)
        {
            var errorLog = this.GetErrorLog(instance.ProcID, instance.ID, Constants.ErrorProfile.All);
            this.RetryError(errorLog);
        }
    }
}