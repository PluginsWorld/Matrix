using System.Collections.Generic;
using System.Net;
using Matrix.Models;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;

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
                    ServerResult result = GetServerResult(server.APIKey);
                    UnturnedChat.Say(caller, $"[{count}] {server.name}, {server.ip}:{server.port} [{result.players}/{result.maxplayers}]");
                    count++;
                }
            }
        }
        public ServerResult GetServerResult(string APIKey)
        {
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString($"https://unturned-servers.net/api/?object=servers&element=detail&key={WebUtility.UrlEncode(APIKey)}");
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ServerResult>(json);
            }
        }
    }
}