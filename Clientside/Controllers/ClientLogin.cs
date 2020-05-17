using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clientside.Enums;
using Clientside.Helpers;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.Controllers {
    public class ClientLogin : Script {
        private Player _localPlayer;

        private int _cameraLogin;
        private bool _switchingPlayer = false;

        public ClientLogin() {
            _localPlayer = Player.LocalPlayer;

            Events.Add("StartPlayerSwitch", StartPlayerSwitch);
            Events.Add("StopPlayerSwitch", StopPlayerSwitch);
        }

        private void StartPlayerSwitch(object[] args) {
            _switchingPlayer = true;

            RAGE.Game.Network.NetworkFadeOutEntity(_localPlayer.Handle, true, false);

            Chat.Activate(false);
            Chat.Show(false);

            var rotX = 0f;
            var rotY = 0f;
            var rotZ = 0f;
            var fieldOfView = 90.0f;

            _cameraLogin = RAGE.Game.Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), 3353.261f, 5154.649f, 50f, rotX, rotY, rotZ, fieldOfView, true, 2);
            RAGE.Game.Cam.PointCamAtCoord(_cameraLogin, 3459.142f, 5096.136f, 25);

            RAGE.Game.Graphics.StartScreenEffect("SwitchHUDIn", 1000, true);

            RAGE.Game.Cam.SetCamActive(_cameraLogin, true);
            RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);

            Common.RemoveHUDElements();
        }

        private void StopPlayerSwitch(object[] args) {
            if (_switchingPlayer) {
                _switchingPlayer = false;

                RAGE.Game.Misc.ClearOverrideWeather();
                RAGE.Game.Misc.ClearWeatherTypePersist();

                RAGE.Game.Cam.DestroyCam(_cameraLogin, true);
                RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);

                RAGE.Game.Network.NetworkFadeInEntity(_localPlayer.Handle, true, 0);

                Chat.Activate(true);
                Chat.Show(true);

                RAGE.Game.Graphics.StopAllScreenEffects();

                Common.RemoveHUDElements();

                SetupLocalAI();
            }
        }

        private void SetupLocalAI() {
            RAGE.Game.Ped.RemoveScenarioBlockingAreas();

            RAGE.Game.Streaming.SetPedPopulationBudget(0);
            RAGE.Game.Streaming.SetVehiclePopulationBudget(-1);

            RAGE.Game.Ped.SetCreateRandomCops(false);
            RAGE.Game.Ped.SetCreateRandomCopsNotOnScenarios(false);
            RAGE.Game.Ped.SetCreateRandomCopsOnScenarios(false);

            RAGE.Game.Vehicle.SetRandomBoats(true);
            RAGE.Game.Vehicle.SetRandomTrains(true);
            RAGE.Game.Vehicle.SetGarbageTrucks(true);

            RAGE.Game.Vehicle.SetAllVehicleGeneratorsActive();
            RAGE.Game.Vehicle.SetAllLowPriorityVehicleGeneratorsActive(true);

            RAGE.Game.Vehicle.SetNumberOfParkedVehicles(-1);
            RAGE.Game.Vehicle.DisplayDistantVehicles(true);
            RAGE.Game.Graphics.DisableVehicleDistantlights(false);
        }
    }
}
