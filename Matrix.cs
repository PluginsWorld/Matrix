using Matrix.Models;
using Rocket.Core.Plugins;
using Logger = Rocket.Core.Logging.Logger;

namespace Matrix
{
    public class Matrix : RocketPlugin<MatrixConfiguration>
    {
        public static Matrix Instance;
        protected override void Load()
        {
            int count = 0;
            base.Load();
            Instance = this;
            Logger.Log("Loading Servers...");
            foreach (Server server in Configuration.Instance.Servers)
            {
                count++;
                Logger.Log($"Name: {server.name} IP: {server.ip} Port: {server.port}");
            }

            if (count == 1)
            {
                Logger.Log("Loaded 1 server");
            }
            else
            {
                Logger.Log($"Loaded {count} servers");
            }
        }

        protected override void Unload()
        {
            base.Unload();
            Instance = null;
        }
        
    }
}