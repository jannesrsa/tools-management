using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using SourceCode.Hosting.Client.BaseAPI;
using SourceCode.Tools.Management.Extensions;

namespace SourceCode.Tools.Management.Model
{
    internal class ServerConfiguration
    {
        private const string _encryptKey = "ê┘£◄q1₧▐";
        private const string _k2ConfigFile = "K2ServerSettings.set";
        private const string _k2ConfigSection = "K2ServerSettings";

        public ServerConfiguration()
        {
            CopyConfigToIsolatedFile();
        }

        public static SCConnectionStringBuilder[] GetAllServerConfiguration()
        {
            var servers = new List<SCConnectionStringBuilder>();

            using (var userStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                if (userStorage.GetFileNames(_k2ConfigFile).Contains(_k2ConfigFile))
                {
                    using (var userStream = new IsolatedStorageFileStream(_k2ConfigFile, FileMode.Open, userStorage))
                    using (var cryptostreamLoad = new CryptoStream(userStream, Decryptor(), CryptoStreamMode.Read))
                    {
                        var xpathDoc = new XPathDocument(cryptostreamLoad);
                        var xpathNavigator = xpathDoc.CreateNavigator();
                        var connections = xpathNavigator.Select("//connectionstring");

                        foreach (XPathNavigator connection in connections)
                        {
                            var server = new SCConnectionStringBuilder(connection.GetAttribute("connectionstring", string.Empty));
                            servers.Add(server);
                        }
                    }
                }
            }

            return servers.ToArray();
        }

        public void CopyConfigToIsolatedFile()
        {
            var connectionStrings = GetAllServerConfigurationFromOldConfigFile();

            foreach (var connectioString in connectionStrings)
            {
                CreateNewServerConfig(connectioString);
            }
        }

        public void CreateNewServerConfig(SCConnectionStringBuilder hostServerConfig)
        {
            hostServerConfig.ThrowIfNull(nameof(hostServerConfig));

            string name = hostServerConfig.GetDisplayName();
            string connectionstring = hostServerConfig.ConnectionString;

            CreateIsolatedStorage();

            using (var userStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                XmlDocument k2ConfDoc = null;

                using (var userStream = new IsolatedStorageFileStream(_k2ConfigFile, FileMode.Open, userStorage))
                using (var cryptostreamLoad = new CryptoStream(userStream, Decryptor(), CryptoStreamMode.Read))
                {
                    k2ConfDoc = new XmlDocument();
                    k2ConfDoc.Load(cryptostreamLoad);
                }

                using (var userStream = new IsolatedStorageFileStream(_k2ConfigFile, FileMode.Create, userStorage))
                using (var cryptostream = new CryptoStream(userStream, Encryptor(), CryptoStreamMode.Write))
                {
                    try
                    {
                        var connectionStringsElement = k2ConfDoc.SelectSingleNode("/configuration/connectionstrings");
                        var currentNode = connectionStringsElement.SelectSingleNode(string.Format("connectionstring[@name='{0}']", name));

                        if (currentNode != null)
                        {
                            connectionStringsElement.RemoveChild(currentNode);
                        }

                        var connStringNode = k2ConfDoc.CreateElement("connectionstring");
                        connectionStringsElement.PrependChild(connStringNode);

                        connStringNode.SetAttribute("name", name);
                        connStringNode.SetAttribute("connectionstring", connectionstring);

                        k2ConfDoc.Save(cryptostream);
                        userStream.Flush();
                    }
                    finally
                    {
                        k2ConfDoc = null;
                    }
                }
            }
        }

        public string GetCurrentServerConfiguration()
        {
            string currentServer = string.Empty;
            CreateIsolatedStorage();

            using (var userStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                XPathDocument xpathDoc;

                var userStream = new IsolatedStorageFileStream(_k2ConfigFile, FileMode.Open, userStorage);

                var cryptostreamDecr = new CryptoStream(userStream, Decryptor(), CryptoStreamMode.Read);

                try
                {
                    xpathDoc = new XPathDocument(cryptostreamDecr);
                }
                catch (Exception)
                {
                    if (userStream != null)
                    {
                        userStream.Dispose();
                    }

                    userStream = new IsolatedStorageFileStream(_k2ConfigFile, FileMode.Open, userStorage);
                    userStream.SetLength(0);
                    userStream.Flush();
                    userStream.Close();
                    CreateIsolatedStorage();
                }
                finally
                {
                    if (userStream != null)
                    {
                        userStream.Dispose();
                    }
                }

                cryptostreamDecr = null;

                using (var isolatedStorageStream = new IsolatedStorageFileStream(_k2ConfigFile, FileMode.Open, userStorage))
                using (var cryptostreamDecr2 = new CryptoStream(isolatedStorageStream, Decryptor(), CryptoStreamMode.Read))
                {
                    //string what = new StreamReader(cryptostreamDecr).ReadToEnd();
                    xpathDoc = new XPathDocument(cryptostreamDecr2);
                    var xpathNavigator = xpathDoc.CreateNavigator();
                    var connection = xpathNavigator.SelectSingleNode("/configuration/connectionstrings/connectionstring");

                    if (connection == null)
                    {
                        return null;
                    }

                    currentServer = connection.GetAttribute("connectionstring", string.Empty);
                }
            }

            return currentServer;
        }

        public void Remove(SCConnectionStringBuilder hostServerConfig)
        {
            hostServerConfig.ThrowIfNull(nameof(hostServerConfig));

            string name = hostServerConfig.GetDisplayName();
            CreateIsolatedStorage();

            using (var userStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                XmlDocument k2ConfDoc = null;

                using (var userStream = new IsolatedStorageFileStream(_k2ConfigFile, FileMode.Open, userStorage))
                using (CryptoStream cryptostreamLoad = new CryptoStream(userStream, Decryptor(), CryptoStreamMode.Read))
                {
                    k2ConfDoc = new XmlDocument();
                    k2ConfDoc.Load(cryptostreamLoad);
                }

                var deleteNode = k2ConfDoc.SelectSingleNode(string.Format("//configuration/connectionstrings/connectionstring[@name='{0}']", name));

                if (deleteNode != null)
                {
                    var connectionstringsNode = k2ConfDoc.SelectSingleNode("//configuration/connectionstrings");
                    connectionstringsNode.RemoveChild(deleteNode);

                    using (var userStream = new IsolatedStorageFileStream(_k2ConfigFile, FileMode.Create, userStorage))
                    using (var cryptostream = new CryptoStream(userStream, Encryptor(), CryptoStreamMode.Write))
                    {
                        try
                        {
                            k2ConfDoc.Save(cryptostream);
                            userStream.Flush();
                        }
                        finally
                        {
                            k2ConfDoc = null;
                        }
                    }
                }
            }
        }

        private static void CreateIsolatedStorage()
        {
            using (var userStorage = IsolatedStorageFile.GetUserStoreForAssembly())
            using (var userStream = new IsolatedStorageFileStream(_k2ConfigFile, FileMode.OpenOrCreate, userStorage))
            {
                if (userStream.Length == 0)
                {
                    using (var cryptostream = new CryptoStream(userStream, Encryptor(), CryptoStreamMode.Write))
                    {
                        // Create the doc for the first time
                        var doc = new XmlDocument();

                        var configuration = doc.CreateElement("configuration");
                        doc.AppendChild(configuration);

                        var connectionstrings = doc.CreateElement("connectionstrings");
                        configuration.AppendChild(connectionstrings);

                        doc.Save(cryptostream);
                        doc = null;
                    }
                }
            }
        }

        private static ICryptoTransform Decryptor()
        {
            using (var DES = new DESCryptoServiceProvider())
            {
                //A 64 bit key and IV is required for this provider.
                //Set secret key For DES algorithm.
                DES.Key = ASCIIEncoding.ASCII.GetBytes(_encryptKey);
                //Set initialization vector.
                DES.IV = ASCIIEncoding.ASCII.GetBytes(_encryptKey);
                return DES.CreateDecryptor();
            }
        }

        private static ICryptoTransform Encryptor()
        {
            using (var DES = new DESCryptoServiceProvider())
            {
                //A 64 bit key and IV is required for this provider.
                //Set secret key For DES algorithm.
                DES.Key = ASCIIEncoding.ASCII.GetBytes(_encryptKey);
                //Set initialization vector.
                DES.IV = ASCIIEncoding.ASCII.GetBytes(_encryptKey);
                return DES.CreateEncryptor();
            }
        }

        private static SCConnectionStringBuilder[] GetAllServerConfigurationFromOldConfigFile()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var strings = new List<SCConnectionStringBuilder>();

            foreach (ConnectionStringSettings connectionStringSetting in config.ConnectionStrings.ConnectionStrings)
            {
                if (connectionStringSetting.ProviderName == _k2ConfigSection)
                {
                    strings.Insert(0, new SCConnectionStringBuilder(connectionStringSetting.ConnectionString));
                }
            }

            return strings.ToArray();
        }
    }
}