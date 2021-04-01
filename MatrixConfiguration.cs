using System.Collections.Generic;
using Matrix.Models;
using Rocket.API;

namespace Matrix
{
    public class MatrixConfiguration : IRocketPluginConfiguration
    {
        public bool isProxy;
        public string HubIP;
        public ushort HubPort;
        public string HubPassword;
        public float HubDelay;
        public string allServerPermission;
        public bool enableRegionsCompatiblity;
        public List<Server> Servers;
        public void LoadDefaults()
        {
            isProxy = true;
            HubIP = "127.0.0.1";
            HubPort = 27018;
            HubPassword = "";
            HubDelay = 0f;
            allServerPermission = "matrix.servers.all";
            enableRegionsCompatiblity = false;
            Servers = new List<Server>() {new Server {name = "main", ip = "127.0.0.1", port = 27015, permission = "matrix.server.main", Delay = 0f, password = "", APIKey = ""}};
        }
    }
}