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
using Serverside.Models;
using Serverside.Services;
using Colors = System.Drawing.Color;

namespace Serverside.Controllers {
    // These can be spoofed?
    // client.SocialClubName
    // client.Serial

    public class ServerAuth : Script {
        private StorageDB<Account> _accountStorage;
        private StorageDB<Character> _characterStorage;

        public ServerAuth() {

        }

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart() {
            _accountStorage = new StorageDB<Account>();
            _characterStorage = new StorageDB<Character>();

            NAPI.Server.SetAutoSpawnOnConnect(true);

            NAPI.Server.SetAutoRespawnAfterDeath(false);

            NAPI.Server.SetDefaultSpawnLocation(new Vector3(224.1551f, -859.2976f, 30.11921f), 342.6247f);

            var accounts = _accountStorage.Get();
            var characters = _characterStorage.Get();

            Logging.Log($"    Accounts: {accounts.Count}");
            Logging.Log($"    Characters: {characters.Count}");

            accounts = null;
            characters = null;
        }

        [ServerEvent(Event.PlayerConnected)]
        public void ServerEvent_PlayerConnected(Client player) {
            Logging.Log($"{player.SocialClubName} ({player.Address}): Connected.");

            player.Dimension = player.Handle.Value;

            player.TriggerEvent("StartPlayerSwitch");

            NAPI.Task.Run(() => {
                player.ExecuteBrowser("login", true, new object[] {
                    "setUsername", player.SocialClubName
                });
            }, 1000);
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void ServerEvent_PlayerDisconnected(Client client, DisconnectionType disconnectionType, string reason) {
            if (client.HasData("CharacterId")) {
                try {
                    string characterId = client.GetData("CharacterId").ToString();
                    var character = _characterStorage.Get(characterId);
                    if (character != null) {
                        character.Model = client.Model;
                        character.Health = client.Health;
                        character.Armor = client.Armor;

                        character.Hunger = client.GetHunger();
                        character.Thirst = client.GetThirst();
                        character.Stress = client.GetStress();

                        _characterStorage.Update(character);

                        Logging.Log($"{client.SocialClubName} ({client.Address}): Saved (characterId: {characterId})");
                    }
                    else {
                        Logging.Log($"{client.SocialClubName} ({client.Address}): Unsaved (characterId: {characterId})");
                    }
                }
                catch (Exception ex) {
                    Logging.Log($"{client.SocialClubName} ({client.Address}) (PlayerDisconnected): {ex.Message}");
                }
            }

            var disconnectionString = "DROPPED";

            if (disconnectionType == DisconnectionType.Left) {
                disconnectionString = "LEFT";
            }
            else if (disconnectionType == DisconnectionType.Timeout) {
                disconnectionString = "TIMEOUT";
            }
            else if (disconnectionType == DisconnectionType.Kicked) {
                disconnectionString = "KICKED";
            }

            if (!string.IsNullOrEmpty(reason)) {
                Logging.Log($"{client.SocialClubName} ({client.Address}): Disconnected ({disconnectionString}) - {reason}");
            }
            else {
                Logging.Log($"{client.SocialClubName} ({client.Address}): Disconnected ({disconnectionString})");
            }
        }

        [RemoteEvent("RequestPlayerLogin")]
        public void RequestPlayerLogin(Client player, string username, string password) {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
                player.ExecuteBrowserJS("login", "loginError", new object[] {
                    "You must provide a username and password. Please try again."
                });
                return;
            }

            username = username.Trim();
            password = password.Trim();

            if (username.Contains(" ") || password.Contains(" ")) {
                player.ExecuteBrowserJS("login", "loginError", new object[] {
                    "Username and password cannot contain spaces. Please try again."
                });
                return;
            }

            var account = (_accountStorage.Find(x => x.SocialClubName.ToLower() == username.ToLower())).FirstOrDefault();
            if (account == null) {
                account = new Account() {
                    SocialClubName = player.SocialClubName,
                    Password = password.ToMD5Hash()
                };

                account = _accountStorage.Create(account);

                Logging.Log($"{player.SocialClubName} ({player.Address}):     Created account.");
            }

            if (account.Password != password.ToMD5Hash()) {
                Logging.Log($"{player.SocialClubName} ({player.Address}):     LOGIN FAILED.");

                player.ExecuteBrowserJS("login", "loginError", new object[] {
                    "Username or password is incorrect. Please try again."
                });
                return;
            }
            else {
                Logging.Log($"{player.SocialClubName} ({player.Address}):     LOGIN SUCCESS.");
            }

            var ipAddress = account.IPs.FirstOrDefault(x => x.Address == player.Address);
            if (ipAddress == null) {
                account.IPs.Add(new IPAddress() {
                    Address = player.Address
                });

                account = _accountStorage.Update(account);

                Logging.Log($"{player.SocialClubName} ({player.Address}):     Updated account.");
            }

            player.SetData("AccountId", account.Id);

            var characters = _characterStorage.Find(x => x.AccountId == account.Id);

            var charactersData = new List<dynamic>();

            foreach (var character in characters) {
                charactersData.Add(new {
                    id = character.Id,
                    fullName = character.FullName,
                    age = character.Age,
                    bank = character.Bank.ToString("C"),
                    cash = character.Cash.ToString("C")
                });
            }

            Logging.Log($"{player.SocialClubName} ({player.Address}):     charactersData: {charactersData.Count}");

            player.DestroyBrowser("login", true);

            player.ExecuteBrowser("characters", true, new object[] {
                "setCharacters", NAPI.Util.ToJson(charactersData)
            });
        }

        [RemoteEvent("RequestCharacter")]
        public void RequestCharacter(Client player, string characterId) {
            if (!player.HasData("AccountId")) {
                player.ExecuteBrowserJS("characters", "createError", new object[] {
                    "Something went wrong (1). Please try again."
                });
                return;
            }

            if (string.IsNullOrEmpty(characterId)) {
                player.ExecuteBrowserJS("characters", "createError", new object[] {
                    "Something went wrong (2). Please try again."
                });
                return;
            }

            var accountId = (string)player.GetData("AccountId");

            Character character = _characterStorage.Get(characterId);

            if (character == null) {
                player.ExecuteBrowserJS("characters", "createError", new object[] {
                    "Something went wrong (3). Please try again."
                });
                return;
            }

            player.ApplyCharacter(character);

            player.TriggerEvent("StopPlayerSwitch");

            player.DestroyBrowser("characters", false);
            player.ExecuteBrowser("statusBars", false);

            player.Dimension = 0;
        }

        [RemoteEvent("DeleteCharacter")]
        public void DeleteCharacter(Client player, string characterId) {
            if (!player.HasData("AccountId")) {
                player.ExecuteBrowserJS("characters", "createError", new object[] {
                    "Something went wrong (1). Please try again."
                });
                return;
            }

            if (string.IsNullOrEmpty(characterId)) {
                player.ExecuteBrowserJS("characters", "createError", new object[] {
                    "Something went wrong (2). Please try again."
                });
                return;
            }

            var accountId = (string)player.GetData("AccountId");

            Character character = _characterStorage.Get(characterId);

            if (character == null) {
                player.ExecuteBrowserJS("characters", "createError", new object[] {
                    "Something went wrong (3). Please try again."
                });
                return;
            }

            // TODO FINISH
        }

        [RemoteEvent("CreateCharacter")]
        public void CreateCharacter(Client player, string firstName, string middleName, string lastName, string bio, string dateOfBirth) {
            Logging.Log($"CreateCharacter");

            if (!player.HasData("AccountId")) {
                player.ExecuteBrowserJS("characters", "createError", new object[] {
                    "Something went wrong (1). Please try again."
                });
                return;
            }

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(middleName) || string.IsNullOrEmpty(lastName)) {
                player.ExecuteBrowserJS("characters", "createError", new object[] {
                    "You must provide a first, last and middle name. Please try again."
                });
                return;
            }

            var accountId = (string)player.GetData("AccountId");

            var characters = _characterStorage.Find(x => x.FirstName.ToLower() == firstName.ToLower() && x.LastName.ToLower() == lastName.ToLower());

            if (characters.Count() > 0) {
                player.ExecuteBrowserJS("characters", "createError", new object[] {
                    "A character with that first and last name already exists. Please try again."
                });
                return;
            }

            Character character = new Character(accountId) {
                FirstName = firstName,
                MiddleName = lastName,
                LastName = middleName,
                Bio = bio,
                BirthDate = DateTime.Parse(dateOfBirth).ToUniversalTime(),
                InventoryId = ObjectId.GenerateNewId().ToString()
            };

            Logging.Log($"        {character.FirstName}");
            Logging.Log($"        {character.MiddleName}");
            Logging.Log($"        {character.LastName}");
            Logging.Log($"        {character.Bio}");
            Logging.Log($"        {character.BirthDate}");

            character = _characterStorage.Create(character);

            player.ApplyCharacter(character);

            player.TriggerEvent("StopPlayerSwitch");
            player.Dimension = 0;

            player.DestroyBrowser("characters", false);
            player.ExecuteBrowser("statusBars", false);
        }
    }
}
