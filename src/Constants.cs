using System;

namespace SourceCode.Tools.Management
{
    /// <summary>
    /// Contains all constants used in the app
    /// </summary>
    internal static class Constants
    {
        public static class DataTable
        {
            public static class Activity
            {
                public const string Name = "Name";
            }

            public static class Instance
            {
                public const string Folio = "Folio";
                public const string Id = "ID";
                public const string Originator = "Originator";
                public const string StartDate = "Start Date";
                public const string Status = "Status";
                public const string Version = "Version";
            }
        }

        public static class EnvironmentField
        {
            public const string K2ViewflowUrl = "K2 Viewflow URL";
            public const string WebServiceUrl = "Web Service URL";
            public const string WebServiceUrlSsl = "Web Service URL SSL";
        }

        public static class ErrorProfile
        {
            public static int All = 1;
        }

        public static class ImageName
        {
            public const string Process = "Process";
            public const string ProcessFolder = "ProcessFolder";
            public const string ProcessInstance = "ProcessInstance";
            public const string ProcSet = "ProcSet";
        }

        public static class ServiceInstance
        {
            public static Guid WorkflowReporting = new Guid("ef7310e5-b14f-464d-b0f3-a37d6c367620");
        }

        public static class Url
        {
            public const string ViewFlow = "/ViewFlow/ViewFlow.aspx?ProcessID=";
        }
    }
}