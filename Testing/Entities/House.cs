using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.Enums;

namespace Testing.Entities {
    public class House {

        public int Id { get; set; }

        public string Name { get; set; }

        public Vector3 Entrance { get; set; }

        public int Building { get; set; }
    }
}
