using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Common.Helpers {
    public static class Logging {

        public static void Log() {
            Console.WriteLine($"");
        }

        public static void Log(string message) {
            var currentTime = DateTime.Now;
            var longTimeString = currentTime.ToLongTimeString().PadLeft(11);

            Console.WriteLine($"[{longTimeString.Pastel(Color.Orange)}] {message}");
        }

        public static void Log(string message, Color fontColor) {
            Log(message.Pastel(fontColor));
        }
    }
}
