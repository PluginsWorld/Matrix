using System.Collections.Generic;
using System.Linq;
using Matrix.Models;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;

namespace Matrix.Commands
{
    public class CommandServer : IRocketCommand
    {

        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "server";

        public string Help => "Connects you to a specified server";

        public string Syntax => "";

        public List<string> Aliases => new List<string>() { "serv" };

        public List<string> Permissions => new List<string>() { "matrix.server" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, "Please specify a server to join");
                return;
            }

            Server server =
                Matrix.Instance.Configuration.Instance.Servers.FirstOrDefault(k => k.name.ToLower() == command[0].ToLower());
            if (server == null)
            {
                UnturnedChat.Say(caller, $"This server does not exist");
                return;
            }

            UnturnedPlayer player = (UnturnedPlayer) caller;
            if (!player.HasPermission(server.permission))
            {
                UnturnedChat.Say(caller, "You do not have permission to join this server");
                return;
            }

            UnturnedChat.Say(caller, $"Sending you to {server.name}");
            Matrix.Instance.SendPlayer(server, player);
        }
    }
}