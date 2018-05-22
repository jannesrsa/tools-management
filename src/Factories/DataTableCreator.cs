using System;
using System.Collections.Generic;
using System.Data;
using SourceCode.Tools.Management.Extensions;
using SourceCode.Workflow.Management;

namespace SourceCode.Tools.Management.Factories
{
    internal static class DataTableCreator
    {
        public static DataTable GetDataTable(IEnumerable<ProcessInstance> processInstances)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(Constants.DataTable.Instance.Id);
            dataTable.Columns.Add(Constants.DataTable.Instance.Folio);
            dataTable.Columns.Add(Constants.DataTable.Instance.StartDate, typeof(DateTime));
            dataTable.Columns.Add(Constants.DataTable.Instance.Status);
            dataTable.Columns.Add(Constants.DataTable.Instance.Originator);
            dataTable.Columns.Add(Constants.DataTable.Instance.Version, typeof(int));

            foreach (var processInstance in processInstances.EmptyIfNull())
            {
                var newRow = dataTable.NewRow();

                newRow[Constants.DataTable.Instance.Id] = processInstance.ID;
                newRow[Constants.DataTable.Instance.Folio] = processInstance.Folio;
                newRow[Constants.DataTable.Instance.StartDate] = processInstance.StartDate;
                newRow[Constants.DataTable.Instance.Status] = processInstance.GetProcessInstanceStatus().ToString();
                newRow[Constants.DataTable.Instance.Originator] = processInstance.Originator;
                newRow[Constants.DataTable.Instance.Version] = processInstance.Process.VersionNumber;

                dataTable.Rows.Add(newRow);
            }

            return dataTable;
        }

        internal static DataTable GetDataTable(Activity[] activities)
        {
            var dataTable = new DataTable();

            dataTable.Columns.Add(Constants.DataTable.Activity.Name);

            foreach (var activity in activities.EmptyIfNull())
            {
                var newRow = dataTable.NewRow();

                newRow[Constants.DataTable.Activity.Name] = activity.Name;

                dataTable.Rows.Add(newRow);
            }

            return dataTable;
        }
    }
}