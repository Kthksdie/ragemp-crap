using System;
using System.Linq;
using Common.Extensions;
using Common.Helpers;
using GTANetworkAPI;
using Colors = System.Drawing.Color;

namespace Serverside.Events {
    class ServerEvents : Script {
        public ServerEvents() {
            //Logging.Log("Started.");
        }

        [ServerEvent(Event.FirstChanceException)]
        public void ServerEvent_FirstChanceException(Exception exception) {
            Logging.Log($"First Chance Exception:");
            Logging.Log($"    Message: {exception.Message}");
            Logging.Log($"    Stack Trace: {exception.StackTrace}");
        }

        [ServerEvent(Event.UnhandledException)]
        public void ServerEvent_UnhandledException(Exception exception) {
            Logging.Log($"Unhandled Exception:");
            Logging.Log($"    Message: {exception.Message}");
            Logging.Log($"    Stack Trace: {exception.StackTrace}");
        }

        [ServerEvent(Event.ChatMessage)]
        public void ServerEvent_ChatMessage(Client client, string message) {
            Logging.Log($"{client.SocialClubName} ({client.Address}): {message}");
        }

        [ServerEvent(Event.PlayerSpawn)]
        public void ServerEvent_PlayerDeath(Client client) {
            Logging.Log($"{client.SocialClubName} ({client.Address}): Spawned");
        }

        [ServerEvent(Event.PlayerDeath)]
        public void ServerEvent_PlayerDeath(Client client, Client killer, uint reason) {
            var deathReasons = Enum.GetValues(typeof(DeathReason)).Cast<uint>().ToList();
            var weaponHashes = Enum.GetValues(typeof(WeaponHash)).Cast<uint>().ToList();

            var diedVia = "Unknown";

            if (deathReasons.Contains(reason)) {
                diedVia = $"{(DeathReason)reason}";
            }
            else if (weaponHashes.Contains(reason)) {
                diedVia = $"{(WeaponHash)reason}";
            }

            Logging.Log($"{client.SocialClubName} ({client.Address}): Died via {diedVia}");
        }

        [ServerEvent(Event.VehicleDamage)]
        public void ServerEvent_VehicleDamage(Vehicle vehicle, float bodyHealthLoss, float engineHealthLoss) {
            try {
                var bodyHealth = NAPI.Vehicle.GetVehicleBodyHealth(vehicle.Handle);
                var engineHealth = NAPI.Vehicle.GetVehicleEngineHealth(vehicle.Handle);

                Logging.Log($"{vehicle.DisplayName}: Damaged");
                Logging.Log($"    Body:   {bodyHealthLoss} / {bodyHealth}");
                Logging.Log($"    Engine: {engineHealthLoss} / {engineHealth}");
            }
            catch (Exception ex) {
                Logging.Log($"Exception (ServerEvent_VehicleDamage): {ex.Message}");
            }
        }
    }
}
