using System;
using System.Collections.Generic;
using System.Text;
using Clientside.Enums;
using Clientside.Helpers;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.Controllers {
    public class ClientBasicNeeds : Script {
        private Player _localPlayer = Player.LocalPlayer;
        private int _currentHealth = 0;
        private int _currentArmor = 0;

        public ClientBasicNeeds() {
            Events.OnPlayerSpawn += OnPlayerSpawnEvent;

            Tick += OnTick;
        }

        private void OnTick(List<TickNametagData> nametags) {
            var health = _localPlayer.GetHealth();
            var armour = _localPlayer.GetArmour();

            if (_currentHealth != health) {
                _currentHealth = health;
                Browser.ExecuteFunctionEvent(new object[] { "package://statics/statusBars/index.html", "UpdateBasicNeedLevel", "HealthLevel", _currentHealth });
            }

            if (_currentArmor != armour) {
                _currentArmor = armour;
                Browser.ExecuteFunctionEvent(new object[] { "package://statics/statusBars/index.html", "UpdateBasicNeedLevel", "ArmorLevel", _currentArmor });
            }
        }

        private void OnPlayerSpawnEvent(CancelEventArgs cancel) {
            Common.RemoveHUDElements();
        }
    }
}
