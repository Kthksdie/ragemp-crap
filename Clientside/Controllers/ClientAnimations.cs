using System;
using System.Collections.Generic;
using System.Text;
using Clientside.Enums;
using Clientside.Helpers;
using RAGE;
using RAGE.Elements;
using static RAGE.Events;

namespace Clientside.Controllers {
    public class ClientAnimations : Script {
        private Player _localPlayer = Player.LocalPlayer;

        public ClientAnimations() {
            Chat.Output("Loaded Clientside.Controllers.ClientAnimations");

            Events.Add("PlayAnimation", PlayAnimation);
            Events.Add("PlayAnimationSet", PlayAnimationSet);

            Events.Add("ClearPedTasks", ClearPedTasks);
            Events.Add("ClearPedTasksImmediately", ClearPedTasksImmediately);

            //Browser.CreateBrowserEvent(new object[] { "package://statics/chat/index.html" });
        }

        private void PlayAnimation(object[] args) {
            Chat.Output($"{Player.LocalPlayer.Name} ({Player.LocalPlayer.Id}): PlayAnimation");

            var pedHandle = Convert.ToInt32(args[0]);
            var animLibrary = args[1].ToString();
            var animName = args[2].ToString();
            var animFlags = Convert.ToInt32(args[3]);
            var duration = Convert.ToInt32(args[4]);

            Chat.Output($"    pedHandle: {pedHandle}");
            Chat.Output($"    animLibrary: {animLibrary}");
            Chat.Output($"    animName: {animName}");
            Chat.Output($"    animFlags: {animFlags}");
            Chat.Output($"    duration: {duration}");

            

            //RAGE.Game.Ai.TaskPlayAnim(pedHandle, animLibrary, animName, 8f, -8f, duration, animFlags, 8f, false, false, false);

            //if (LoadAnimDict(animLibrary)) {
            //    Chat.Output($"    LoadAnimDict1: {animLibrary}");
            //    //RAGE.Game.Ai.TaskPlayAnim(pedHandle, animLibrary, animName, 8f, -8f, duration, animFlags, 8f, false, false, false);
            //}
            //else {
            //    Chat.Output($"    LoadAnimDict2: {animLibrary}");
            //}

            //AI::TASK_PLAY_ANIM(syncActors.at(actorIndex)->getActorPed(), animation.animLibrary, animation.animName, 8.0f, -8.0f, duration, getDefaultAnimationFlag().id, 8.0f, 0, 0, 0);
        }

        private void PlayAnimationSet(object[] args) {
            try {
                Chat.Output($"PlayAnimation");

                var pedHandle = Convert.ToInt32(args[0]);
                var animations = (dynamic)args[1];
                var looped = Convert.ToBoolean(args[2]);
                var deltaZ = Convert.ToSingle(args[3]);
                
                Chat.Output($"pedHandle: {pedHandle}");
                Chat.Output($"animations: {animations.Count}");
                Chat.Output($"looped: {looped}");
                Chat.Output($"deltaZ: {deltaZ}");

                var sceneId = RAGE.Game.Ped.CreateSynchronizedScene(_localPlayer.Position.X, _localPlayer.Position.Y, _localPlayer.Position.Z + deltaZ, 0f, 0f, _localPlayer.GetRotation(2).Z, 2);
                RAGE.Game.Ped.SetSynchronizedSceneLooped(sceneId, looped);

                //AI::TASK_SYNCHRONIZED_SCENE(syncActors.at(actorIndex)->getActorPed(), m_sceneId, animation.animLibrary, animation.animName, 1000.0, -4.0, 64, 0, 0x447a0000, 0);

                foreach (var animation in animations) {

                    Chat.Output($"{animation.Name}: {animation.Value}");
                    RAGE.Game.Ai.TaskSynchronizedScene(pedHandle, sceneId, animation.Value.ToString(), animation.Name.ToString(), 1000f, -4f, 64, 0, 0x447a0000, 0);
                }

                RAGE.Game.Ped.SetSynchronizedScenePhase(sceneId, 0f);
            }
            catch (Exception ex) {
                Chat.Output($"PlayAnimation: {ex.Message}");
            }
        }

        private void ClearPedTasks(object[] args) {
            Chat.Output($"ClearPedTasks: Id: {_localPlayer.Id} RemoteId: {_localPlayer.RemoteId} Handle: {_localPlayer.Handle}");

            RAGE.Game.Ai.ClearPedTasks(_localPlayer.Handle);
        }

        private void ClearPedTasksImmediately(object[] args) {
            Chat.Output($"ClearPedTasksImmediately: Id: {_localPlayer.Id} RemoteId: {_localPlayer.RemoteId} Handle: {_localPlayer.Handle}");

            RAGE.Game.Ai.ClearPedTasksImmediately(_localPlayer.Handle);
        }
    }
}
