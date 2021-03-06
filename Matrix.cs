using System.Collections;
using System.Net;
using Matrix.Models;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;
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

            U.Events.OnPlayerConnected += PlayerConnected;
        }

        private void PlayerConnected(UnturnedPlayer player)
        {
            if (Configuration.Instance.isProxy)
            {
                SendToHub(player);
            }
        }

        protected override void Unload()
        {
            base.Unload();
            Instance = null;
        }

        public void SendPlayer(Server server, UnturnedPlayer player, bool delay = false)
        {
            StartCoroutine(SwitchDelay(server, player, delay));
        }

        public void SendToHub(UnturnedPlayer player)
        {
            bool delay = false;
            StartCoroutine(HubDelay(player, delay));
        }
        private IEnumerator SwitchDelay(Server server, UnturnedPlayer player, bool delay)
        {
            if (delay)
            {
                UnturnedChat.Say(player, $"Changing server in {server.Delay}", Color.green);
            }
            string serverIP = "";
            bool isIPV4 = false;
            
            if (IPAddress.TryParse(server.ip, out IPAddress Address))
            {
                switch (Address.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        serverIP = Address.ToString();
                        isIPV4 = true;
                        break;
                }
            }
            if (!isIPV4)
            {
                serverIP = Dns.GetHostAddresses(server.ip)[0].ToString();
            }
            if (delay)
            {
                yield return new WaitForSeconds(server.Delay);
            }
            player.Player.sendRelayToServer(Parser.getUInt32FromIP(serverIP), server.port, server.password, false);
        }

        private IEnumerator HubDelay(UnturnedPlayer player, bool delay)
        {
            string hubIP = "";
            bool IPV4 = false;
            if (delay)
            {
                UnturnedChat.Say(player, $"Changing server in {Configuration.Instance.HubDelay}", Color.green);
            }
            if (IPAddress.TryParse(Configuration.Instance.HubIP, out IPAddress Address))
            {
                switch (Address.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        hubIP = Address.ToString();
                        IPV4 = true;
                        break;
                }
            }
            if (!IPV4)
            {
                hubIP = Dns.GetHostAddresses(Configuration.Instance.HubIP)[0].ToString();
            }
            if (delay)
            {
                yield return new WaitForSeconds(Configuration.Instance.HubDelay);
            }
            player.Player.sendRelayToServer(Parser.getUInt32FromIP(hubIP), Configuration.Instance.HubPort, Configuration.Instance.HubPassword, false);
        }
    }
}