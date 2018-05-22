using System.Collections.Generic;
using SourceCode.Tools.Management.Extensions;

namespace SourceCode.Tools.Management.DataAccessLayer.Model
{
    /// <summary>
    /// DataField
    /// </summary>
    public class DataField
    {
        internal const string ProcessDataField = "Data";
        internal const string ProcessXMLField = "XML";

        public string FieldType { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        internal static DataField Convert(SourceCode.Workflow.Management.ProcessDataField value)
        {
            var returnVal = new DataField
            {
                Name = value.Name,
                FieldType = DataField.ProcessDataField
            };

            if (value.InitialValue != null)
            {
                returnVal.Value = value.InitialValue.ToString();
            }

            return returnVal;
        }

        internal static DataField Convert(SourceCode.Workflow.Management.ProcessXMLField value)
        {
            var returnVal = new DataField
            {
                Name = value.Name,
                FieldType = DataField.ProcessXMLField
            };

            if (value.InitialValue != null)
            {
                returnVal.Value = value.InitialValue.ToString();
            }

            return returnVal;
        }

        internal static IEnumerable<DataField> GetEnumerable(IEnumerable<SourceCode.Workflow.Management.ProcessDataField> values)
        {
            foreach (var value in values.EmptyIfNull())
            {
                yield return DataField.Convert(value);
            }
        }

        internal static IEnumerable<DataField> GetEnumerable(IEnumerable<SourceCode.Workflow.Management.ProcessXMLField> values)
        {
            foreach (var value in values.EmptyIfNull())
            {
                yield return DataField.Convert(value);
            }
        }
    }
}