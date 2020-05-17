using System;
using System.Linq;
using Common.Extensions;
using Common.Helpers;
using GTANetworkAPI;
using Serverside.Enums;
using Serverside.Extensions;
using Colors = System.Drawing.Color;

namespace Serverside.Controllers {
    public class ServerInjuries : Script {
        // Job Ideas:
        //    Bus Driver
        //        - Drive around checkpoints, limit speed on Bus to 45 mph?
        //          Full lap of checkpoints generates pay. Pay based on distince between all checkpoints?
        public ServerInjuries() {

        }

        [RemoteEvent("PlayerBoneDamage")]
        public void PlayerBoneDamage(Client player, object[] args) {
            var boneIndex = args.FirstOrDefault();
            if (boneIndex != null) {
                var bone = (Bones)boneIndex;
                var boneName = bone.GetBoneName();

                if (!string.IsNullOrEmpty(boneName)) {
                    Logging.Log($"{player.SocialClubName} ({player.Address}): Damanged on {boneName}");

                    player.AddStress(5);
                }
            }
        }
    }
}
