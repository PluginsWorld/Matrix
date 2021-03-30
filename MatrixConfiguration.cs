using System.Collections.Generic;
using Matrix.Models;
using Rocket.API;

namespace Matrix
{
    public class MatrixConfiguration : IRocketPluginConfiguration
    {
        public bool enableRegionsCompatiblity;
        public List<Server> Servers;
        public void LoadDefaults()
        {
            enableRegionsCompatiblity = false;
            Servers = new List<Server>() {new Server {name = "main", ip = "127.0.0.1", port = 27015}};
        }
    }
}