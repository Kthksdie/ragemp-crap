using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Common.Extensions;
using Common.Helpers;
using GTANetworkAPI;
using Serverside.Entities;
using Serverside.Enums;
using Serverside.Extensions;
using Serverside.Services;
using Colors = System.Drawing.Color;

namespace Serverside.Controllers {
    class ServerVehicles : Script {
        private List<VehicleSpawnPoint> _vehicleSpawnPoints = new List<VehicleSpawnPoint>();

        public ServerVehicles() {

        }

        [ServerEvent(Event.ResourceStart)]
        public void SE_ResourceStart() {
            if (File.Exists("./data/SpawnPoints_Vehicles.json")) {
                var spawnPointsString = File.ReadAllText("./data/SpawnPoints_Vehicles.json");
                _vehicleSpawnPoints = NAPI.Util.FromJson<List<VehicleSpawnPoint>>(spawnPointsString);
            }

            Logging.Log($"Loaded {_vehicleSpawnPoints.Count} vehicle spawnpoints.");

            //SpawnVehicles();
        }

        // Hotwire
        // Index: 93487 veh@std@ds@base
        // Open/Lockpick?
        // 7848 missarmenian3_tryopendoor
        // 7849 missarmenian3_tryopendoor
        // 8328 misscommon@locked_door
        // 8329 misscommon@locked_door
        // 10885 missheistfbi3b_ig2
        // 


        public void SpawnVehicles() {
            var random = new Random();
            foreach (var vehicleSpawnPoint in _vehicleSpawnPoints) {
                var vehicleName = vehicleSpawnPoint.Vehicles[random.Next(0, vehicleSpawnPoint.Vehicles.Count - 1)];

                var vehicleHash = NAPI.Util.GetHashKey(vehicleName);
                var plateNumber = $"{Strings.Random(7)}L";

                var randomVehicleColor = Enum<VehicleMetallicColors>.Random();

                var position = new Vector3(vehicleSpawnPoint.X, vehicleSpawnPoint.Y, vehicleSpawnPoint.Z);

                var canSpawn = true;
                foreach (var existingVehicle in NAPI.Pools.GetAllVehicles()) {
                    if (existingVehicle.Position.DistanceToSquared2D(position) < 2) {
                        canSpawn = false;
                        break;
                    }
                }

                if (canSpawn) {
                    var vehicle = NAPI.Vehicle.CreateVehicle(vehicleHash, position, vehicleSpawnPoint.Heading, (int)randomVehicleColor, (int)randomVehicleColor);

                    if (vehicle.Exists) {
                        vehicle.Locked = false;
                        vehicle.EngineStatus = false;
                        vehicle.NumberPlateStyle = (int)PlateStyles.YellowOnBlack;
                        vehicle.NumberPlate = plateNumber;
                    }
                }
            }

            Logging.Log($"Spawned {NAPI.Pools.GetAllVehicles().Count} vehicles.");
        }
    }
}
