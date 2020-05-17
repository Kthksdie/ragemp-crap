using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Common.Extensions {
    public static class StringExtensions {
        public static string Colorize(this string value, Color color) {
            var colorName = color.Name.Substring(0, 1).ToLower();

            return $"~{colorName}~{value}~w~";
        }
    }
}
