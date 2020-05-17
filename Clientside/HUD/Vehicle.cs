using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Clientside.Helpers;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.HUD {
    public class Vehicle : Script {
        // Implement server side file validation with this?
        // send contents of *.cs files to server to validate?
        // https://stackoverflow.com/questions/10520048/calculate-md5-checksum-for-a-file

        private Player _localPlayer = Player.LocalPlayer;

        private static string _meString = "";

        public Vehicle() {
            Events.Add("Show3DText", Show3DText);

            Events.Tick += OnTick;

            Events.OnPlayerEnterVehicle += OnPlayerEnterVehicleEvent;
            Events.OnPlayerLeaveVehicle += OnPlayerLeaveVehicleEvent;
        }

        private void OnPlayerEnterVehicleEvent(RAGE.Elements.Vehicle vehicle, int seatId) {
            Browser.ExecuteFunctionEvent(new object[] { "package://statics/statusBars/index.html", "showSpeedo" });
        }

        private void OnPlayerLeaveVehicleEvent() {
            Browser.ExecuteFunctionEvent(new object[] { "package://statics/statusBars/index.html", "hideSpeedo" });
        }

        public void UpdateInfos() {
            //var len = (int)this.Request.ContentLength;
            //byte[] b = new byte[len];
            //await this.Request.Body.ReadAsync(b, 0, len);

        }

        private void OnTick(List<TickNametagData> nametags) {

            if (_localPlayer.IsSittingInAnyVehicle()) {
                var vehicle = _localPlayer.Vehicle;
                var speedMph = vehicle.GetSpeed() * 2.2369f;
                if (speedMph < 0) {
                    speedMph = 0;
                }

                var rpm = (vehicle.GetSpeed() / RAGE.Game.Vehicle.GetVehicleModelMaxSpeed(vehicle.Model)) * 10000;
                if (rpm < 0) {
                    rpm = 0;
                }

                var gear = 0;

                Browser.ExecuteFunctionEvent(new object[] { "package://statics/statusBars/index.html", "setSpeedo", speedMph, rpm, gear });
            }

            //var screenX = 0;
            //var screenY = 0;

            //RAGE.Game.Graphics.GetActiveScreenResolution(ref screenX, ref screenY);

            //var posX = 0f;
            //var posY = 0f;

            //RAGE.Game.Graphics.GetScreenCoordFromWorldCoord(playerPosition.X, playerPosition.Y, playerPosition.Z, ref posX, ref posY);

            //posX = screenX * posX;
            //posY = screenY * posY;

            //if (!string.IsNullOrEmpty(_meString)) {
            //    Browser.ExecuteFunctionEvent(new object[] { "Show3DMessage", "MeMessage", $"X: {screenX}, X: {screenY} - X: {posX}, X: {posY}", posX, posY });
            //}

            //RAGE.Game.Vehicle.GetVehicleAcceleration
            //RAGE.Game.Vehicle.GetVehicleClassMaxAcceleration

            //if (Player.LocalPlayer.IsOnScreen()) {
            //    var textPosition1 = playerPosition; textPosition1.Z = textPosition1.Z + 0.1f;
            //    var textPosition2 = playerPosition; textPosition2.Z = textPosition2.Z + 0.2f;
            //    var textPosition3 = playerPosition; textPosition3.Z = textPosition3.Z + 0.3f;

            //    DrawText3D(Player.LocalPlayer.Name, textPosition3, Color.White, RAGE.Game.Font.ChaletLondon);
            //}

            //foreach (var item in RAGE.Elements.Entities.Objects.All) {
            //    if (playerPosition.DistanceToSquared2D(item.Position) <= 5) {
            //        DrawText3D($"Item", playerPosition, Color.White, RAGE.Game.Font.ChaletComprimeCologne);
            //    }
            //}

            //var outPosition = new Vector3();
            //var outNodeId = 0;
            //var outHeading = 0f;
            //var nodeType0 = 0;
            //var nodeType1 = 1;
            //if (RAGE.Game.Pathfind.GetClosestVehicleNodeWithHeading(playerPosition.X, playerPosition.Y, playerPosition.Z, outPosition, ref outHeading, nodeType0, 3, 0)) {
            //    DrawText3D("Vehicle Node 0", outPosition, Color.White, RAGE.Game.Font.ChaletLondon);
            //}

            //if (RAGE.Game.Pathfind.GetClosestVehicleNodeWithHeading(playerPosition.X, playerPosition.Y, playerPosition.Z, outPosition, ref outHeading, nodeType1, 3, 0)) {
            //    DrawText3D("Vehicle Node 1", outPosition, Color.White, RAGE.Game.Font.ChaletLondon);
            //}
            //if (!string.IsNullOrEmpty(_meString)) {
            //    playerPosition.Z = playerPosition.Z - 0.45f;

            //    DrawText3D(_meString, playerPosition, Color.White, RAGE.Game.Font.ChaletComprimeCologne);
            //}

            //if (RAGE.Game.Pathfind.GetNthClosestVehicleNodeWithHeading(playerPosition.X, playerPosition.Y, playerPosition.Z, 0, outPosition, ref outHeading, ref outNodeId, 0, 0f, 0f)) {
            //    DrawText3D($"Node {outNodeId}", outPosition, Color.White, RAGE.Game.Font.Monospace);
            //}

            //if (RAGE.Game.Pathfind.GetRandomVehicleNode(playerPosition.X, playerPosition.Y, playerPosition.Z, 5f, true, false, false, outPosition, ref outNodeId)) {
            //    DrawText3D($"Node {outNodeId}", outPosition, Color.White, RAGE.Game.Font.Pricedown);
            //}

            //RAGE.Game.Misc.GetModelDimensions
            //var timeString = GameTimeString();
            //var _resX = 0;
            //var _resY = 0;

            //RAGE.Game.Entity.IsEntityOnScreen()
            //var screenX = 0f;
            //var screenY = 0f;

            //var playerPosition = Player.LocalPlayer.Position;

            //RAGE.Game.Graphics.GetActiveScreenResolution(ref _resX, ref _resY);

            //var vehicle = Player.LocalPlayer.Vehicle;

            //if (!vehicle.IsNull) {
            //    var currentSpeed = RAGE.Game.Utils.Ceil(vehicle.GetSpeed() * 2.2369f);

            //    RAGE.Game.UIText.Draw($"{currentSpeed} mph", new Point(_resX - 25, 50), 0.5f, Color.White, RAGE.Game.Font.ChaletLondon, false);
            //}
        }

        private void Show3DText(object[] args) {
            Chat.Output($"{Player.LocalPlayer.Name} ({Player.LocalPlayer.Id}): Show3DText");

            var seconds = Convert.ToInt32(args[0]);
            var message = args[1].ToString();

            var duration = TimeSpan.FromSeconds(seconds);
            var endTime = DateTime.UtcNow.Add(duration);
            Chat.Output($"Show3DText.1: {endTime}");

            _meString = message;
        }
    }
}
