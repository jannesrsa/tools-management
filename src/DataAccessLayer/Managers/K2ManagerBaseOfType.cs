using SourceCode.Hosting.Client.BaseAPI;

namespace SourceCode.Tools.Management.DataAccessLayer.Managers
{
    /// <summary>
    /// Add a BaseAPI type to simplify getting the server
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SourceCode.Tools.Management.DataAccessLayer.Managers.K2ManagerBase" />
    internal abstract class K2ManagerBaseOfType<T> : K2ManagerBase
        where T : BaseAPI, new()
    {
        protected T GetServer()
        {
            T server = new T();

            server.CreateConnection();
            server.Connection.Open(_k2Client.K2Config.HostServerConnectionString);

            return server;
        }
    }
}