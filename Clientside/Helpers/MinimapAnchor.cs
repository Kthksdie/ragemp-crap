using System;
using System.Collections.Generic;
using System.Text;
using RAGE;

namespace Clientside.Helpers {
    public class MinimapAnchor {

        public MinimapAnchor() {
            Refresh();
        }

        public void Refresh() {
            var safeZone = RAGE.Game.Graphics.GetSafeZoneSize();
            var safeZone_X = 1.0f / 20.0f;
            var safeZone_Y = 1.0f / 20.0f;

            var aspectRatio = RAGE.Game.Graphics.GetAspectRatio(false);
            var res_X = 0;
            var res_Y = 0;

            AspectRatio = aspectRatio;

            RAGE.Game.Graphics.GetActiveScreenResolution(ref res_X, ref res_Y);

            var xScale = 1.0f / res_X;
            var yScale = 1.0f / res_Y;

            Width = (float)(xScale * (res_X / (4 * aspectRatio)));
            Height = (float)(yScale * (res_Y / 5.674));

            Left_X = (float)(xScale * (res_X * (safeZone_X * ((RAGE.Game.Misc.Absf(safeZone - 1.0f)) * 10f))));
            Bottom_Y = (float)(1.0f - yScale * (res_Y * (safeZone_Y * ((RAGE.Game.Misc.Absf(safeZone - 1.0f)) * 10f))));

            Right_X = Left_X + Width;
            Top_Y = Bottom_Y - Height;

            X = Left_X;
            Y = Top_Y;

            X_Unit = xScale;
            Y_Unit = yScale;
        }

        public float AspectRatio { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public float X_Unit { get; set; }

        public float Y_Unit { get; set; }

        public float Left_X { get; set; }

        public float Bottom_Y { get; set; }

        public float Right_X { get; set; }

        public float Top_Y { get; set; }
    }
}
