using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace Matrix.Commands
{
    public class CommandReload : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "matrix";

        public string Help => "Base Matrix Plugin Command";

        public string Syntax => "";

        public List<string> Aliases => new List<string>() { "m", "mx" };

        public List<string> Permissions => new List<string>() { "matrix" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer) caller;
            if (command.Length < 1)
            {
                
            }

            if (command[0].ToLower() == "reload")
            {
                if (player.HasPermission("matrix.reload"))
                {
                    UnturnedChat.Say(caller, "Matrix >> Reloading Matrix");
                    Matrix.Instance.Configuration.Unload();
                    Matrix.Instance.Configuration.Load();
                    UnturnedChat.Say(caller, "Matrix >> Matrix Reload Complete");
                }
                else
                {
                    UnturnedChat.Say(caller, "Matrix >> You do not have the right permissions to execute this");
                }
            }

            if (command[0].ToLower() == "help")
            {
                UnturnedChat.Say(caller, "Matrix >> /server <Server Name>");
                UnturnedChat.Say(caller, "Matrix >> This sends you to a server that is specified if it exists");
            }
        }
    }
}