using SourceCode.Tools.Management.Extensions;

namespace SourceCode.Tools.Management.DataAccessLayer.Managers
{
    /// <summary>
    /// Base class for managers wrapping K2 API methods
    /// </summary>
    internal abstract class K2ManagerBase
    {
        protected K2Client _k2Client;

        internal void SetK2Client(K2Client k2Client)
        {
            k2Client.ThrowIfNull(nameof(k2Client));
            _k2Client = k2Client;
        }
    }
}