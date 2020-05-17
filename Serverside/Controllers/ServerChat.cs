using System;
using System.Linq;
using Common.Extensions;
using Common.Helpers;
using GTANetworkAPI;
using Serverside.Enums;
using Serverside.Extensions;
using Colors = System.Drawing.Color;

namespace Serverside.Controllers {
    public class ServerChat : Script {
        public ServerChat() {
            NAPI.Server.SetGlobalDefaultCommandMessages(true);
            NAPI.Server.SetGlobalServerChat(false);
        }

        //[RemoteEvent("playerChat")]
        //public void PlayerChat(Client player, string message) {
        //    Logging.Log($"{player.SocialClubName} ({player.Address}): {message}");

        //    player.TriggerEvent("Send_ToChat", message);
        //}
    }
}
