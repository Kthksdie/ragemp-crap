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
    public class ServerAnimations : Script {
        private List<Animation> _animations = new List<Animation>();
        private List<AnimationSet> _animationSets = new List<AnimationSet>();

        public ServerAnimations() {
            
        }

        [ServerEvent(Event.ResourceStart)]
        public void SE_ResourceStart() {
            if (File.Exists("./data/Animations.json")) {
                var animationString = File.ReadAllText("./data/Animations.json");
                _animations = NAPI.Util.FromJson<List<Animation>>(animationString);
            }

            Logging.Log($"Loaded {_animations.Count} animations.");

            if (File.Exists("./data/AnimationSets.json")) {
                var animationSetsString = File.ReadAllText("./data/AnimationSets.json");
                _animationSets = NAPI.Util.FromJson<List<AnimationSet>>(animationSetsString);
            }

            Logging.Log($"Loaded {_animationSets.Count} animationSets.");
        }

        [Command("playanimation", Alias = "pa")]
        public void PlayAnimation(Client player, int animIndex) {
            Logging.Log($"{player.SocialClubName} ({player.Address}): {animIndex}");

            var animation = _animations.FirstOrDefault(x => x.Index == animIndex);

            if (animation != null) {
                Logging.Log($"{player.SocialClubName} ({player.Address}): PlayAnimation {animation.Library}");

                player.PlayAnimation(animation.Library, animation.Name, (int)AnimationFlags.LOOP_FLAG1);
                //player.TriggerEvent("PlayAnimation", player.Handle.Value, animation.Library, animation.Name, AnimationFlags.LOOP_FLAG1, animation.Duration);
            }
            else {
                Logging.Log($"{player.SocialClubName} ({player.Address}): PlayAnimation {animIndex} - Not Found");
            }
        }

        [Command("playanimationset", Alias = "pas")]
        public void PlayAnimationSet(Client player, int animIndex) {
            Logging.Log($"{player.SocialClubName} ({player.Address}): {animIndex}");

            var animationSet = _animationSets.FirstOrDefault(x => x.Index == animIndex);

            if (animationSet != null) {
                Logging.Log($"{player.SocialClubName} ({player.Address}): PlayAnimation {animationSet.Index}");

                var animations = new Dictionary<string, string>();

                foreach (var index in animationSet.ActorAnims) {
                    var animation = _animations.FirstOrDefault(x => x.Index == index);
                    if (animation != null) {
                        animations.Add(animation.Name, animation.Library);
                    }
                }

                var looped = false;

                player.TriggerEvent("PlayAnimationSet", player.Handle.Entity<Ped>().Handle.Value, animations, looped, animationSet.DeltaZ);
            }
            else {
                Logging.Log($"{player.SocialClubName} ({player.Address}): PlayAnimation {animIndex} - Not Found");
            }
        }

        [Command("cancelanimation", Alias = "ca")]
        public void CancelAnimation(Client player) {
            Logging.Log($"{player.SocialClubName} ({player.Address}): CancelAnimation");

            //NAPI.Ped.ClearPedTasks(player.Handle.Entity<Ped>());

            //player.StopAnimation();

            player.TriggerEvent("ClearPedTasks");
        }
    }
}
