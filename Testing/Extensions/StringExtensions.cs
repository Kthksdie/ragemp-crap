using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Extensions {
    public static class StringExtensions {
        public static List<string> ToList(this string value, char seperator) {
            return value.Split(new char[] { seperator }, StringSplitOptions.None).ToList();
        }
    }
}
