using System;
using System.Collections.Generic;
using System.Threading;
using Serverside.Enums;
using Serverside.Extensions;
using Serverside.Models;
using Serverside.Services;
using Common.Extensions;
using Common.Helpers;
using GTANetworkAPI;
using Colors = System.Drawing.Color;

namespace Serverside.Commands {
    class General : Script {
        private readonly StorageDB<Account> _accountStorage;
        private readonly StorageDB<Character> _characterStorage;

        // NAPI.Native.SendNativeToAllPlayers

        public General() { // 1234
            _accountStorage = new StorageDB<Account>();
            _characterStorage = new StorageDB<Character>();

            //Logging.Log($"Started.".Pastel(Colors.Green));
        }

        [Command("sethealth", Alias = "sethp")]
        public void SetHealth(Client player, int toValue) {
            Logging.Log($"{player.SocialClubName} ({player.Address}): Health {toValue}");

            player.SetHealth(toValue);
        }

        [Command("setarmour", Alias = "setar")]
        public void SetArmour(Client player, int toValue) {
            Logging.Log($"{player.SocialClubName} ({player.Address}): Armor {toValue}");

            player.SetArmor(toValue);
        }

        [Command("sethunger")]
        public void SetHunger(Client player, int toValue) {
            Logging.Log($"{player.SocialClubName} ({player.Address}): Hunger: {toValue}");

            player.SetHunger(toValue);
        }

        [Command("setthirst")]
        public void SetThirst(Client player, int toValue) {
            Logging.Log($"{player.SocialClubName} ({player.Address}): Thirst: {toValue}");

            player.SetThirst(toValue);
        }

        [Command("setstress")]
        public void SetStress(Client player, int toValue) {
            Logging.Log($"{player.SocialClubName} ({player.Address}): Stress: {toValue}");

            player.SetStress(toValue);
        }

        [Command("me", "something simple", GreedyArg = true)]
        public void Me(Client player, string message) {
            Logging.Log($"{player.SocialClubName} ({player.Address}): {message}");
            var duration = 5;

            player.TriggerEvent("Show3DText", duration, message);
        }

        [Command("disconnect", Alias = "dc", Description = "Kick yourself from the server.", Hide = false)]
        public void CMD_Reconnect(Client client) {
            Logging.Log($"{client.SocialClubName} ({client.Address}): Disconnecting...");
            client.SendChatMessage($"Disconnecting...");
            client.SendChatMessage($"Use ~g~F1~w~ to reconnect.");

            NAPI.Player.KickPlayer(client, "Disconnected");
        }

        [Command("balances", Alias = "bal", Description = "Get your available Cash and Bank balances.", Hide = false)]
        public void CMD_Balances(Client client) {
            if (!client.HasData("CharacterId")) {
                return;
            }

            var characterId = (string)client.GetData("CharacterId");
            var character = _characterStorage.Get(characterId);

            if (character != null) {
                Logging.Log($"{client.SocialClubName} ({client.Address}): Balances");
                Logging.Log($"    Bank: {character.Bank}");
                Logging.Log($"    Cash: {character.Cash}");

                client.SendChatMessage($"    Bank: {character.Bank}");
                client.SendChatMessage($"    Cash: {character.Cash}");
            }
        }

        [Command("respawn")]
        public void CMD_Respawn(Client client) {
            Logging.Log($"{client.SocialClubName} ({client.Address}): Respawned");

            if (NAPI.Player.IsPlayerDead(client)) {
                NAPI.Player.SpawnPlayer(client, client.Position.Around(0));
            }

            client.SendChatMessage($"Respawned");
        }

        [Command("tele", Alias = "tp")]
        public void CMD_Tele(Client client, float x, float y, float z) {
            Logging.Log($"{client.SocialClubName} ({client.Address}):");
            Logging.Log($"    X: {client.Position.X}, Y: {client.Position.Y}, Z: {client.Position.Z}, Heading: {client.Heading}");

            client.Position = new Vector3(x, y, z);
        }

        [Command("time", Alias = "t")]
        public void CMD_Time(Client client, int hours, int minutes, int seconds) {
            Logging.Log($"{client.SocialClubName} ({client.Address}): {hours}:{minutes}:{seconds}");

            NAPI.World.SetTime(hours, minutes, seconds);
        }

        [Command("where")]
        public void CMD_Where(Client client) {
            Logging.Log($"{client.SocialClubName} ({client.Address}):");
            Logging.Log($"    X: {client.Position.X}, Y: {client.Position.Y}, Z: {client.Position.Z}, Heading: {client.Heading}");

            client.SendChatMessage($"X: {client.Position.X}, Y: {client.Position.Y}, Z: {client.Position.Z}, Heading: {client.Heading}");
        }

        // LS Air Port: X: -1037.725, Y: -2737.637, Z: 20.16929, Heading: 326.7851

        // Favorites: deviant, impaler, imperator, rapidgt3, deluxo, deathbike2, revolter
        //    monster3
        // Flying: oppressor2
        // RC Vehicles: rcbandito

        [Command("spawnvehicle", Alias = "sv", Description = "Spawns a vehicle", Hide = false)]
        public void CMD_SpawnVehicle(Client client, string vehicleName) {
            if (client.IsInVehicle) {
                client.WarpOutOfVehicle();
            }

            if (client.HasData("SpawnedVehicle")) {
                var spawnedVehicle = (Vehicle)client.GetData("SpawnedVehicle");
                if (spawnedVehicle.Exists) {
                    spawnedVehicle.Delete();
                }
            }

            Logging.Log($"Creating vehicle...");
            client.SendChatMessage("Creating vehicle...");

            var vehicleHash = NAPI.Util.GetHashKey(vehicleName);
            var plateNumber = Strings.Random(8);

            var randomVehicleColor = Enum<VehicleMetallicColors>.Random();

            var vehicle = NAPI.Vehicle.CreateVehicle(vehicleHash, client.Position.Around(5), 0, (int)randomVehicleColor, (int)randomVehicleColor);

            if (vehicle.Exists) {
                vehicle.Locked = false;
                vehicle.EngineStatus = false;
                vehicle.NumberPlateStyle = (int)PlateStyles.YellowOnBlack;
                vehicle.NumberPlate = plateNumber;

                Logging.Log($"Successfully spawned '{vehicle.DisplayName.Pastel(Colors.Green)}' with plate number '{plateNumber.Pastel(Colors.Green)}'");

                client.SendChatMessage($"Successfully spawned '{vehicle.DisplayName.Colorize(Colors.Green)}' with plate number '{plateNumber.Colorize(Colors.Green)}'");

                client.SetData("SpawnedVehicle", vehicle);

                client.SetIntoVehicle(vehicle.Handle, -1);
            }
            else {
                Logging.Log($"Failed to spawn '{vehicleName.Pastel(Colors.Green)}'");

                client.SendChatMessage($"Failed to spawn '{vehicleName.Colorize(Colors.Green)}'");
            }
        }

        [Command("lockvehicle")]
        public void CMD_LockVehicle(Client client) {
            if (client.HasData("SpawnedVehicle")) {
                var spawnedVehicle = (Vehicle)client.GetData("SpawnedVehicle");
                if (!spawnedVehicle.IsNull) {
                    //client.PlayAnimation("anim@mp_player_intmenu@key_fob@", "fob_click", (int)AnimFlags.UpperBodyOnly);

                    if (spawnedVehicle.Locked) {
                        // AUDIO::PLAY_SOUND_FROM_ENTITY(-1, "Bell", PLAYER::PLAYER_PED_ID(), "LIFT_NORMAL_SOUNDSET", 0, 0);

                        NAPI.Native.SendNativeToPlayersInRange(spawnedVehicle.Position, 10, Hash.PLAY_SOUND_FROM_ENTITY, -1, "Remote_Control_Open", spawnedVehicle.Handle.Value, "PI_Menu_Sounds", 1, 0);
                    }
                    else {
                        NAPI.Native.SendNativeToPlayersInRange(spawnedVehicle.Position, 10, Hash.PLAY_SOUND_FROM_ENTITY, -1, "Remote_Control_Close", spawnedVehicle.Handle.Value, "PI_Menu_Sounds", 1, 0);
                    }

                    spawnedVehicle.Locked = !spawnedVehicle.Locked;

                    client.SendChatMessage($"Your '{spawnedVehicle.DisplayName.ToUpper().Colorize(Colors.Green)}' is now {(spawnedVehicle.Locked ? "locked" : "unlocked").Colorize(Colors.Green)}");


                }
            }
        }

        [Command("setskin")]
        public void CMD_SetSkin(Client client, string skinName) {
            var skinHash = NAPI.Util.GetHashKey(skinName);

            client.SetSkin(skinHash);
            client.SetDefaultClothes();
        }
    }
}
