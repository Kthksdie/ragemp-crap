using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Extensions;
using Common.Helpers;
using GTANetworkAPI;
using Serverside.Entities;
using Serverside.Enums;
using Serverside.Extensions;
using Colors = System.Drawing.Color;

namespace Serverside.Controllers {
    public class ServerHouses : Script {
        private List<Building> _buildings = new List<Building>();
        private List<House> _houses = new List<House>();

        public ServerHouses() {
            // Bilingsgate Motel (Numbered Rooms 1 to 14)
            // Room #1 - X: 566.173, Y: -1778.154, Z: 29.35305, Heading: 153.1014

            // Perrera Beach Motel (Numbered Rooms 1 to 37, and 69?)
            // Room #1 - X: -1493.635, Y: -668.337, Z: 29.02508, Heading: 144.3236

            // Bayview Lodge (Numbered Rooms 1 to 10)
            // Room #1 - X: -681.8086, Y: 5770.775, Z: 17.511, Heading: 66.10114

            // Dr Am View Motel (Numbered Rooms 7 to 22)
            // Room #7 - X: -102.5702, Y: 6329.817, Z: 31.57619, Heading: 315.9293

            // The Motor Motel (Numbered Rooms 1 to 12)
            // Room #1 - X: 1142.422, Y: 2654.763, Z: 38.15097, Heading: 91.79399
        }

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart() {
            if (File.Exists("./data/Buildings.json")) {
                var animationString = File.ReadAllText("./data/Buildings.json");
                _buildings = NAPI.Util.FromJson<List<Building>>(animationString);
            }

            Logging.Log($"Loaded {_buildings.Count} buildings.");

            if (File.Exists("./data/Houses.json")) {
                var animationSetsString = File.ReadAllText("./data/Houses.json");
                _houses = NAPI.Util.FromJson<List<House>>(animationSetsString);
            }

            Logging.Log($"Loaded {_houses.Count} houses.");

            foreach (var house in _houses) {
                var colShape = NAPI.ColShape.CreateCylinderColShape(house.Entrance, 3, 3);
                var marker = NAPI.Marker.CreateMarker(1, house.Entrance, new Vector3(), new Vector3(), 1, new Color(255, 255, 255, 100));
                var blip = NAPI.Blip.CreateBlip(411, house.Entrance, 0.5f, 0, "Motel");
            }
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void PlayerEnterColshape(ColShape colShape, Client player) {

        }
    }
}
