using System;
using System.Collections.Generic;
using System.Text;
using Clientside.Enums;
using Clientside.Helpers;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.Controllers {
    public class ClientCrouch : Script {
        private Player _localPlayer = Player.LocalPlayer;

        public ClientCrouch() {


            Tick += OnTick;
        }

        private void OnTick(List<TickNametagData> nametags) {
            //RAGE.Input.IsDown
        }
    }
}
