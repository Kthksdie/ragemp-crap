using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers {
    public static class Strings {

        private static Random _random = new Random();

        public static string Random(int stringLength) {
            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++) {
                chars[i] = allowedChars[_random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
