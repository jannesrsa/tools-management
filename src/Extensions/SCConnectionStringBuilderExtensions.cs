using SourceCode.Hosting.Client.BaseAPI;

namespace Tools.Management.Extensions
{
    /// <summary>
    /// Extensions methods for SCConnectionStringBuilder
    /// </summary>
    internal static class SCConnectionStringBuilderExtensions
    {
        public static string GetDisplayName(this SCConnectionStringBuilder connectionStringBuilder)
        {
            var displayName = $"{connectionStringBuilder.Host}:{connectionStringBuilder.Port}";

            if (!string.IsNullOrEmpty(connectionStringBuilder.UserID))
            {
                displayName = $"{displayName} - {connectionStringBuilder.UserID}";
            }

            return displayName.ToUpperInvariant();
        }
    }
}