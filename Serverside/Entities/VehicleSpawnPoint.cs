using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside.Entities {
    public class VehicleSpawnPoint {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Heading { get; set; }

        public List<string> Vehicles { get; set; }
    }
}
