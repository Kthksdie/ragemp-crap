using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside.Entities {
    public class House {

        public int Id { get; set; }

        public string Name { get; set; }

        public Vector3 Entrance { get; set; }

        public int Building { get; set; }
    }
}
