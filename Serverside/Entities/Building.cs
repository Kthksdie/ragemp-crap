using GTANetworkAPI;
using Serverside.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serverside.Entities {
    public class Building {

        public int Id { get; set; }

        public BuildingTypes Type { get; set; }

        public Vector3 Interior { get; set; }

        public Vector3 Exit { get; set; }

        public Vector3 Wardrobe { get; set; }
    }
}
