using System;
using System.Collections.Generic;
using Clientside.Enums;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.Controllers {
    public class ClientClipSets : Script {
        private Player _localPlayer = Player.LocalPlayer;

        public ClientClipSets() {
            Tick += OnTick;

            OnEntityStreamIn += EntityStreamIn;
        }

        private void EntityStreamIn(Entity entity) {
            if (entity.Type == RAGE.Elements.Type.Player) {
                //var entity.GetSharedData("");
            }
        }

        private void OnTick(List<TickNametagData> nametags) {
            if (_localPlayer) {

            }
            var clipSet = _localPlayer.GetSharedData("ClipSet");

        }
    }
}
