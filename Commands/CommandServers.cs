using System.Collections.Generic;
using Matrix.Models;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SteamQueryNet;
using SteamQueryNet.Models;

namespace Matrix.Commands
{
    public class CommandServers : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "servers";

        public string Help => "Lists all connected servers";

        public string Syntax => "";

        public List<string> Aliases => new List<string>() { "servs" };

        public List<string> Permissions => new List<string>() { "matrix.servers" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer) caller;
            foreach (Server server in Matrix.Instance.Configuration.Instance.Servers)
            {
                int count = 1;
                if (player.HasPermission(server.permission) || player.HasPermission(Matrix.Instance.Configuration.Instance.allServerPermission))
                {
                    List<Player> players = new ServerQuery().Connect(server.ip, server.port).GetPlayers();
                    ServerInfo serverInfo = new ServerQuery().Connect(server.ip, server.port).GetServerInfo();
                    UnturnedChat.Say(caller, $"[{count}] {server.name}, {server.ip}:{server.port} [{players.Count}/{serverInfo.MaxPlayers}]");
                    count++;
                }
            }
        }
    }
}