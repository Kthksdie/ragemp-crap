using System;
using System.Collections.Generic;
using System.Text;
using Clientside.Enums;
using Clientside.Helpers;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.Controllers {
    public class ClientChat : Script {
        public ClientChat() {
            //Chat.Output("Loaded Clientside.Controllers.ClientChat");

            //Events.Add("Send_ToChat", Send_ToChat);

            //Browser.CreateBrowserEvent(new object[] { "package://statics/chat/index.html" });
        }

        //private void Send_ToChat(object[] args) {
        //    var message = string.Join(' ', args);

        //    Chat.Output($"{Player.LocalPlayer.Name} ({Player.LocalPlayer.Id}): {message}");
        //}
    }
}
