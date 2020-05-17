using System;
using System.Collections.Generic;
using System.Text;
using Clientside.Enums;
using Clientside.Helpers;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside {
    public class Main : Events.Script {
        private int _streetNameId = (int)RAGE.Game.HudComponent.StreetName;
        private int _vehicleNameId = (int)RAGE.Game.HudComponent.VehicleName;
        private int _reticleId = (int)RAGE.Game.HudComponent.Reticle;

        private float _eX = 0.01255f;
        private float _eY = -0.01260f;

        private float _currentX = 0f;
        private float _currentY = 0f;

        public Main() {
            //Chat.Output("Loaded Clientside.Main");

            //RAGE.Discord.Update("Zuluhotel RP (Dev)", "As Owner");

            Tick += OnTick;
        }

        private void OnTick(List<TickNametagData> nametags) {
            RAGE.Game.Ui.HideHudComponentThisFrame(_streetNameId);
            RAGE.Game.Ui.HideHudComponentThisFrame(_vehicleNameId);
            RAGE.Game.Ui.HideHudComponentThisFrame(_reticleId);

            var position = Player.LocalPlayer.Position;

            if (_currentX != position.X || _currentY != position.Y) {
                _currentX = position.X;
                _currentY = position.Y;

                var posX = Player.LocalPlayer.Position.X;
                var posY = Player.LocalPlayer.Position.Y;

                Browser.ExecuteFunctionEvent(new object[] { "package://statics/statusBars/index.html", "UpdateMiniMap", posX, posY });
            }
        }
    }
}
