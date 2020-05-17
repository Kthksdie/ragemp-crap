using System;
using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Common.Helpers;
using GTANetworkAPI;
using Serverside.Enums;
using Serverside.Extensions;
using Serverside.Models;
using Serverside.Services;

namespace Serverside.Extensions {
    public static class ClientExtensions {
        private static int _maxLevel = 100;
        private static int _minLevel = 0;

        public static void ExecuteBrowser(this Client player, string browserName, bool cursorVisible, params object[] args) {
            // args[0] = url
            // args[1] = cursorVisible

            var eventArgs = new List<object> {
                $"package://statics/{browserName}/index.html",
                cursorVisible
            };

            if (args != null) {
                foreach (var arg in args) {
                    eventArgs.Add(arg);
                }
            }

            player.TriggerEvent("CreateBrowserEvent", eventArgs.ToArray());
        }

        public static void DestroyBrowser(this Client player, string browserName, bool cursorVisible) {
            // args[0] = url
            // args[1] = cursorVisible

            var eventArgs = new List<object> {
                $"package://statics/{browserName}/index.html",
                cursorVisible
            };

            player.TriggerEvent("DestroyBrowserEvent", eventArgs.ToArray());
        }

        public static void ExecuteBrowserJS(this Client player, string browserName, string javascriptFunction, params object[] args) {
            // args[0] = url
            // args[1] = javascriptFunction

            var eventArgs = new List<object>() {
                $"package://statics/{browserName}/index.html",
                javascriptFunction
            };

            if (args != null) {
                foreach (var arg in args) {
                    eventArgs.Add(arg);
                }
            }

            player.TriggerEvent("ExecuteBrowserFunction", eventArgs.ToArray());
        }

        public static void ApplyCharacter(this Client player, Character character) {
            player.SetData("CharacterId", character.Id);

            player.SetSkin(character.Model);

            player.ResetData("BasicNeeds");
            player.StartBasicNeeds();

            player.SetHealth(character.Health);
            player.SetArmor(character.Armor);

            player.SetHunger(character.Hunger);
            player.SetThirst(character.Thirst);
            player.SetStress(character.Stress);

            Logging.Log($"{player.SocialClubName} ({player.Address}): {character.FullName}");
            Logging.Log($"{player.SocialClubName} ({player.Address}):     Age: {character.Age}");
        }

        public static void SetHealth(this Client player, int toValue) {
            player.Health = toValue;

            player.ExecuteBrowserJS("statusBars", "UpdateBasicNeedLevel", new object[] {
                "HealthLevel", player.Health
            });
        }

        public static void SetArmor(this Client player, int toValue) {
            player.Armor = toValue;

            player.ExecuteBrowserJS("statusBars", "UpdateBasicNeedLevel", new object[] {
                "ArmorLevel", player.Armor
            });
        }

        public static bool StartBasicNeeds(this Client player) {
            if (!player.HasData("BasicNeeds")) {
                player.ExecuteBrowser("statusBars", true);

                player.SetHunger(_maxLevel);
                player.SetThirst(_maxLevel);
                player.SetStress(_minLevel);

                player.SetData("BasicNeeds", true);
                return true;
            }
            else {
                return false;
            }
        }

        public static int GetBasicNeed(Client player, string basicNeed) {
            if (!player.HasData("BasicNeeds")) {
                return 0;
            }

            return Convert.ToInt32(player.GetData(basicNeed));
        }

        public static void SetBasicNeed(Client player, string basicNeed, int toValue) {
            if (!player.HasData("BasicNeeds")) {
                return;
            }

            var statusLevel = toValue;

            if (statusLevel > _maxLevel) {
                statusLevel = _maxLevel;
            }

            if (statusLevel < _minLevel) {
                statusLevel = _minLevel;
            }

            player.SetData(basicNeed, statusLevel);

            player.ExecuteBrowserJS("statusBars", "UpdateBasicNeedLevel", new object[] {
                basicNeed, statusLevel
            });
        }

        public static void AddBasicNeed(Client player, string basicNeed, int byValue) {
            if (!player.HasData("BasicNeeds")) {
                return;
            }

            var statusLevel = Convert.ToInt32(player.GetData(basicNeed));
            statusLevel += byValue;

            SetBasicNeed(player, basicNeed, statusLevel);
        }

        public static void SubtractBasicNeed(Client player, string basicNeed, int byValue) {
            if (!player.HasData("BasicNeeds")) {
                return;
            }

            var statusLevel = Convert.ToInt32(player.GetData(basicNeed));
            statusLevel -= byValue;

            SetBasicNeed(player, basicNeed, statusLevel);
        }

        public static int GetHunger(this Client player) {
            return GetBasicNeed(player, "HungerLevel");
        }

        public static void SetHunger(this Client player, int toValue) {
            SetBasicNeed(player, "HungerLevel", toValue);
        }

        public static void AddHunger(this Client player, int byValue) {
            AddBasicNeed(player, "HungerLevel", byValue);
        }

        public static void SubtractHunger(this Client player, int byValue) {
            SubtractBasicNeed(player, "HungerLevel", byValue);
        }

        public static int GetThirst(this Client player) {
            return GetBasicNeed(player, "ThirstLevel");
        }

        public static void SetThirst(this Client player, int toValue) {
            SetBasicNeed(player, "ThirstLevel", toValue);
        }

        public static void AddThirst(this Client player, int byValue) {
            AddBasicNeed(player, "ThirstLevel", byValue);
        }

        public static void SubtractThirst(this Client player, int byValue) {
            SubtractBasicNeed(player, "ThirstLevel", byValue);
        }

        public static int GetStress(this Client player) {
            return GetBasicNeed(player, "StressLevel");
        }

        public static void SetStress(this Client player, int toValue) {
            SetBasicNeed(player, "StressLevel", toValue);
        }

        public static void AddStress(this Client player, int byValue) {
            AddBasicNeed(player, "StressLevel", byValue);
        }

        public static void SubtractStress(this Client player, int byValue) {
            SubtractBasicNeed(player, "StressLevel", byValue);
        }

        public static void ExecuteBrowserEvent_OLD(this Client player, bool cursorVisible, bool destroyBrowser, params object[] args) {
            // args[0] = url
            // args[1] = cursorVisible

            var eventArgs = new List<object> {
                cursorVisible,
                destroyBrowser
            };

            if (args != null) {
                foreach (var arg in args) {
                    eventArgs.Add(arg);
                }
            }

            player.TriggerEvent("ExecuteBrowserEvent", eventArgs.ToArray());
        }

        public static void ExecuteBrowserFunction_OLD(this Client player, string javascriptFunction, params object[] args) {
            var eventArgs = new List<object>();
            eventArgs.Add(javascriptFunction);

            if (args != null) {
                foreach (var arg in args) {
                    eventArgs.Add(arg);
                }
            }

            player.TriggerEvent("ExecuteBrowserFunction", eventArgs.ToArray());
        }
    }
}
