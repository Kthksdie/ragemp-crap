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
    class ServerBasicNeeds : Script {
        public ServerBasicNeeds() {
            // Pickup
            // 10125 missheist_agency2aig_13 pickup_briefcase
            // 14337 pickup_object pickup_low
            // 

            // Dances
            // 7027 mini@strip_club@lap_dance@ld_girl_a_song_a_p1
            // 8291 misschinese2_crystalmazemcs1_cs
            // 9135 missfbi3_sniping
            // 16647 special_ped@mountain_dancer@monologue_1@monologue_1a
            // 16649 special_ped@mountain_dancer@monologue_2@monologue_2a
            // 16650 special_ped@mountain_dancer@monologue_3@monologue_3a
            // 16651 special_ped@mountain_dancer@monologue_4@monologue_4a
        }

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart() {
            var healthStatusDelay = 60000;
            var hungerTicks = 0;
            var thirstTicks = 0;
            var stressTicks = 0;

            NAPI.Task.Run(async () => {
                while (true) {
                    try {
                        var allPlayers = NAPI.Pools.GetAllPlayers();
                        var subtractHunger = false;
                        if (hungerTicks >= 2) {
                            subtractHunger = true;
                            hungerTicks = 0;
                        }
                        else {
                            hungerTicks++;
                        }

                        var subtractThirst = false;
                        if (thirstTicks >= 1) {
                            subtractThirst = true;
                            thirstTicks = 0;
                        }
                        else {
                            thirstTicks++;
                        }

                        var incrementStress = false;
                        if (stressTicks >= 5) {
                            incrementStress = true;
                            stressTicks = 0;
                        }
                        else {
                            stressTicks++;
                        }

                        foreach (var player in allPlayers) {
                            if (!player.Dead) {
                                if (subtractHunger) {
                                    player.SubtractHunger(1);
                                }

                                if (subtractThirst) {
                                    player.SubtractThirst(1);
                                }

                                if (incrementStress) {
                                    player.AddStress(1);
                                }
                            }
                        }
                    }
                    catch (Exception ex) {
                        Logging.Log($"ServerStatus: {ex.Message}");
                    }

                    await System.Threading.Tasks.Task.Delay(healthStatusDelay);
                }
            }, healthStatusDelay);
        }
    }
}
