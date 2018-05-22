using SourceCode.Workflow.Management;

namespace SourceCode.Tools.Management.Extensions
{
    internal static class ProcessInstanceExtensions
    {
        public static string GetProcessInstanceDisplayName(this ProcessInstance instance)
        {
            return $"[{instance.ID}] - {instance.Folio}?";
        }

        public static ProcessInstanceStatus GetProcessInstanceStatus(this ProcessInstance instance)
        {
            int status;
            if (!int.TryParse(instance.Status, out status))
            {
                return ProcessInstanceStatus.Undefined;
            }

            var processInstanceStatus = (ProcessInstanceStatus)status;
            return processInstanceStatus;
        }
    }
}