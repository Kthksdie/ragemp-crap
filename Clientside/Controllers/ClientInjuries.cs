using System;
using System.Collections.Generic;
using Clientside.Enums;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.Controllers {
    public class ClientInjuries : Script {
        public ClientInjuries() {
            Chat.Output("Loaded Clientside.Controllers.ClientInjuries");

            Tick += OnTick;
        }

        private void OnTick(List<TickNametagData> nametags) {
            var playerPedId = RAGE.Game.Player.PlayerPedId();
            int boneId = -1;

            if (RAGE.Game.Ped.GetPedLastDamageBone(playerPedId, ref boneId)) {
                if (boneId != -1) {
                    CallRemote("PlayerBoneDamage", boneId);

                    RAGE.Game.Ped.ClearPedLastDamageBone(playerPedId);
                }
            }
        }
    }
}
