using Serverside.Entities;
using Serverside.Models;
using Serverside.Services;
using Common.Helpers;
using GTANetworkAPI;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Serverside.Extensions;

namespace Serverside {
    public class Main : Script {

        public Main() {

        }

        [ServerEvent(Event.ResourceStart)]
        public void SE_ResourceStart() {
            ScheduleInterval.InHours(DateTime.UtcNow.Hour, 35, 1, () => {
                try {
                    var gameTime = DateTime.Today.Add(NAPI.World.GetTime());

                    Logging.Log($"Time: {gameTime.ToString("hh:mm tt")} / {NAPI.World.GetWeather()}");
                    Logging.Log($"     Players: {NAPI.Pools.GetAllPlayers().Count}");
                    Logging.Log($"    Vehicles: {NAPI.Pools.GetAllVehicles().Count}");
                    Logging.Log($"     Objects: {NAPI.Pools.GetAllObjects().Count}");
                }
                catch (Exception ex) {
                    Logging.Log($"Exception (Serverside.Main): {ex.Message}");
                }
            });
        }
    }
}
