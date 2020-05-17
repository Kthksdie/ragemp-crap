using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.Extensions;

namespace Testing.Helpers {
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
