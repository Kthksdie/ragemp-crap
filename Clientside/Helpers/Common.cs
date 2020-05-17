using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Clientside.Helpers;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.Helpers {
    public static class Common {
        public static int GPSBlipId = 8;

        public static Vector3 GetGPSCoords() {
            var gpsBlipInfoId = RAGE.Game.Ui.GetFirstBlipInfoId(GPSBlipId);

            if (gpsBlipInfoId != 0) {
                return RAGE.Game.Ui.GetBlipCoords(gpsBlipInfoId);
            }

            return null;
        }

        public static void ShowText3D(string name, string text, Vector3 position) {
            var screenWidth = 0;
            var screenHeight = 0;

            RAGE.Game.Graphics.GetActiveScreenResolution(ref screenWidth, ref screenHeight);

            var posX = 0f;
            var posY = 0f;

            RAGE.Game.Graphics.GetScreenCoordFromWorldCoord(position.X, position.Y, position.Z, ref posX, ref posY);

            posX = screenWidth * posX;
            posY = screenHeight * posY;

            Browser.ExecuteFunctionEvent(new object[] { "Show3DMessage", name, text, posX, posY });
        }

        public static void RemoveText3D(string name) {
            Browser.ExecuteFunctionEvent(new object[] { "Remove3DMessage", name });
        }

        public static void DrawText3D(string text, Vector3 position, Color textColor, RAGE.Game.Font textFont) {
            var screenX = 0f;
            var screenY = 0f;
            var fontScale = 0.4f;

            RAGE.Game.Graphics.GetScreenCoordFromWorldCoord(position.X, position.Y, position.Z, ref screenX, ref screenY);

            RAGE.Game.Ui.SetTextOutline();
            RAGE.Game.Ui.SetTextProportional(true);
            RAGE.Game.Ui.SetTextCentre(true);
            RAGE.Game.Ui.SetTextFont((int)textFont);
            RAGE.Game.Ui.SetTextScale(fontScale, fontScale);
            RAGE.Game.Ui.SetTextColour(textColor.R, textColor.G, textColor.B, textColor.A);

            RAGE.Game.Ui.BeginTextCommandDisplayText("STRING");

            RAGE.Game.Ui.AddTextComponentSubstringPlayerName(text);

            RAGE.Game.Ui.EndTextCommandDisplayText(screenX, screenY, 0);

            var rectWidth = GetTextWidth(text, textFont) * 0.85f;
            RAGE.Game.Graphics.DrawRect(screenX, screenY + 0.0155f, rectWidth, 0.03f, 41, 11, 41, 200, 0);
        }

        public static float GetTextWidth(string text, RAGE.Game.Font textFont) {
            RAGE.Game.Ui.BeginTextCommandWidth("STRING");
            RAGE.Game.Ui.AddTextComponentSubstringPlayerName(text);

            RAGE.Game.Ui.SetTextFont((int)textFont);
            RAGE.Game.Ui.SetTextScale(1f, 0.5f);

            return RAGE.Game.Ui.EndTextCommandGetWidth((int)textFont);
        }

        public static string GameTimeString() {
            var hours = RAGE.Game.Clock.GetClockHours();
            var minutes = RAGE.Game.Clock.GetClockMinutes();

            var amPmDesignator = "AM";

            if (hours == 0) {
                hours = 12;
            }
            else if (hours == 12) {
                amPmDesignator = "PM";
            }
            else if (hours > 12) {
                hours -= 12;
                amPmDesignator = "PM";
            }

            var formattedTime = String.Format("{0}:{1:00} {2}", hours, minutes, amPmDesignator);

            return formattedTime;
        }

        public static void RemoveHUDElements() {
            RAGE.Game.Player.SetPlayerHealthRechargeMultiplier(0.0f);

            //RAGE.Game.Player.SetRunSprintMultiplierForPlayer(5f);
            //RAGE.Game.Player.SetSwimMultiplierForPlayer(5f);

            // Remove weapons from the vehicles
            RAGE.Game.Player.DisablePlayerVehicleRewards();

            // Remove the fade out after player's death
            RAGE.Game.Misc.SetFadeOutAfterDeath(false);

            RAGE.Game.Ui.DisplayRadar(false);
            RAGE.Game.Ui.DisplayAreaName(false);
            RAGE.Game.Ui.DisplayCash(false);
        }
    }
}
