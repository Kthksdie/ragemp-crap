using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.Helpers {
    public static class Colors {
        public static List<Color> All() {
            return typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .Select(c => (Color)c.GetValue(null, null))
                .ToList();
        }
    }
}
