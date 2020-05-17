using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Clientside.Helpers;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.HUD {
    public class GPS : Script {
        private int _screenWidth = 0;
        private int _screenHeight = 0;

        private bool _gpsCoords = false;

        public GPS() {
            Chat.Output("Loaded Clientside.HUD.GPS");

            RAGE.Game.Graphics.GetActiveScreenResolution(ref _screenWidth, ref _screenHeight);

            Events.Tick += OnTick;
        }

        private void OnTick(List<TickNametagData> nametags) {
            var gpsCoords = Common.GetGPSCoords();

            if (gpsCoords == null) {
                if (_gpsCoords) {
                    Common.RemoveText3D("gpsCoords");
                    _gpsCoords = false;
                }

                return;
            }

            var streetHeight = GetStreetHeight(gpsCoords);

            gpsCoords.Z = streetHeight;

            var streetNameId = 0;
            var crossingRoadId = 0;

            RAGE.Game.Pathfind.GetStreetNameAtCoord(gpsCoords.X, gpsCoords.Y, gpsCoords.Z, ref streetNameId, ref crossingRoadId);

            var zoneName = RAGE.Game.Zone.GetNameOfZone(gpsCoords.X, gpsCoords.Y, gpsCoords.Z);

            if (streetNameId != 0) {
                var streetName = RAGE.Game.Ui.GetStreetNameFromHashKey((uint)streetNameId);
                var crossingStreetName = "";

                if (crossingRoadId != 0) {
                    crossingStreetName = RAGE.Game.Ui.GetStreetNameFromHashKey((uint)crossingRoadId);
                }

                if (!string.IsNullOrEmpty(crossingStreetName)) {
                    streetName += $" & {crossingStreetName}";
                }

                //Common.DrawText3D($"GPS: {zoneName} | {streetName}", gpsCoords, Color.White, RAGE.Game.Font.ChaletComprimeCologne);

                Common.ShowText3D("gpsCoords", $"GPS: {streetName}", gpsCoords);
                _gpsCoords = true;
            }
            else {
                //Common.DrawText3D($"GPS: {zoneName}", gpsCoords, Color.White, RAGE.Game.Font.ChaletComprimeCologne);

                Common.ShowText3D("gpsCoords", $"GPS: Destination", gpsCoords);
                _gpsCoords = true;
            }
        }



        private float GetStreetHeight(Vector3 currentPosition) {
            var outPosition = new Vector3();
            var nodeType = 0;
            var outGroundZ = 0f;

            if (RAGE.Game.Misc.GetGroundCoordsWhileInAir(currentPosition.X, currentPosition.Y, 1000f, ref outGroundZ, outPosition)) {
                return outGroundZ;
            }

            if (RAGE.Game.Pathfind.GetClosestVehicleNode(currentPosition.X, currentPosition.Y, currentPosition.Z, outPosition, nodeType, 3, 0)) {
                return outPosition.Z;
            }

            return 0f;
        }
    }
}
