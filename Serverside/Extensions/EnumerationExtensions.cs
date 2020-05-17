using Serverside.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Serverside.Extensions {
    public class Enum<T> where T : struct, IConvertible {
        private static Random _random = new Random();

        public static T Random() {
            if (!typeof(T).IsEnum) {
                throw new ArgumentException("T must be an enumerated type");
            }

            var values = Enum.GetValues(typeof(T));

            return (T)values.GetValue(_random.Next(values.Length));
        }

        public static int Count {
            get {
                if (!typeof(T).IsEnum) {
                    throw new ArgumentException("T must be an enumerated type");
                }

                return Enum.GetNames(typeof(T)).Length;
            }
        }
    }

    public static class EnumerationExtensions {
        private static Random _random = new Random();

        public static string Name(this CameraShakes cameraShakes) {
            var cameraShakeNames = new List<string>() {
                "HAND_SHAKE",
                "SMALL_EXPLOSION_SHAKE",
                "MEDIUM_EXPLOSION_SHAKE",
                "LARGE_EXPLOSION_SHAKE",
                "JOLT_SHAKE",
                "VIBRATE_SHAKE",
                "ROAD_VIBRATION_SHAKE",
                "DRUNK_SHAKE",
                "SKY_DIVING_SHAKE",
                "FAMILY5_DRUG_TRIP_SHAKE",
                "DEATH_FAIL_IN_EFFECT_SHAKE"
            };

            return cameraShakeNames[(int)cameraShakes];
        }

        public static string GetBoneName(this Bones bone) {
            switch (bone) {
                case Bones.SKEL_ROOT:
                    break;
                case Bones.FB_R_Brow_Out_000:
                    break;
                case Bones.SKEL_L_Toe0:
                    break;
                case Bones.MH_R_Elbow:
                    break;
                case Bones.SKEL_L_Finger01:
                    break;
                case Bones.SKEL_L_Finger02:
                    break;
                case Bones.SKEL_L_Finger31:
                    break;
                case Bones.SKEL_L_Finger32:
                    break;
                case Bones.SKEL_L_Finger41:
                    break;
                case Bones.SKEL_L_Finger42:
                    break;
                case Bones.SKEL_L_Finger11:
                    break;
                case Bones.SKEL_L_Finger12:
                    break;
                case Bones.SKEL_L_Finger21:
                    break;
                case Bones.SKEL_L_Finger22:
                    break;
                case Bones.RB_L_ArmRoll:
                    break;
                case Bones.IK_R_Hand:
                    break;
                case Bones.RB_R_ThighRoll:
                    break;
                case Bones.SKEL_R_Clavicle:
                    break;
                case Bones.FB_R_Lip_Corner_000:
                    break;
                case Bones.SKEL_Pelvis:
                    return "Waist/Pelvis";
                case Bones.IK_Head:
                    break;
                case Bones.SKEL_L_Foot:
                    return "Left Foot";
                case Bones.MH_R_Knee:
                    break;
                case Bones.FB_LowerLipRoot_000:
                    return "Lower Lip";
                case Bones.FB_R_Lip_Top_000:
                    break;
                case Bones.SKEL_L_Hand:
                    return "Left Hand";
                case Bones.FB_R_CheekBone_000:
                    break;
                case Bones.FB_UpperLipRoot_000:
                    return "Upper Lip";
                case Bones.FB_L_Lip_Top_000:
                    break;
                case Bones.FB_LowerLip_000:
                    break;
                case Bones.SKEL_R_Toe0:
                    break;
                case Bones.FB_L_CheekBone_000:
                    break;
                case Bones.MH_L_Elbow:
                    break;
                case Bones.SKEL_Spine0:
                    break;
                case Bones.RB_L_ThighRoll:
                    break;
                case Bones.PH_R_Foot:
                    break;
                case Bones.SKEL_Spine1:
                    break;
                case Bones.SKEL_Spine2:
                    break;
                case Bones.SKEL_Spine3:
                    return "Chest";
                case Bones.FB_L_Eye_000:
                    break;
                case Bones.SKEL_L_Finger00:
                    break;
                case Bones.SKEL_L_Finger10:
                    break;
                case Bones.SKEL_L_Finger20:
                    break;
                case Bones.SKEL_L_Finger30:
                    break;
                case Bones.SKEL_L_Finger40:
                    break;
                case Bones.FB_R_Eye_000:
                    break;
                case Bones.SKEL_R_Forearm:
                    return "Right Arm";
                case Bones.PH_R_Hand:
                    return "Right Wrist";
                case Bones.FB_L_Lip_Corner_000:
                    break;
                case Bones.SKEL_Head:
                    return "Head";
                case Bones.IK_R_Foot:
                    break;
                case Bones.RB_Neck_1:
                    break;
                case Bones.IK_L_Hand:
                    break;
                case Bones.SKEL_R_Calf:
                    return "Right Leg";
                case Bones.RB_R_ArmRoll:
                    break;
                case Bones.FB_Brow_Centre_000:
                    break;
                case Bones.SKEL_Neck_1:
                    return "Neck";
                case Bones.SKEL_R_UpperArm:
                    return "Right Shoulder";
                case Bones.FB_R_Lid_Upper_000:
                    break;
                case Bones.RB_R_ForeArmRoll:
                    break;
                case Bones.SKEL_L_UpperArm:
                    return "Left Shoulder";
                case Bones.FB_L_Lid_Upper_000:
                    break;
                case Bones.MH_L_Knee:
                    break;
                case Bones.FB_Jaw_000:
                    break;
                case Bones.FB_L_Lip_Bot_000:
                    break;
                case Bones.FB_Tongue_000:
                    return "Tounge";
                case Bones.FB_R_Lip_Bot_000:
                    break;
                case Bones.SKEL_R_Thigh:
                    return "Right Thigh";
                case Bones.SKEL_R_Foot:
                    return "Right Foot";
                case Bones.IK_Root:
                    break;
                case Bones.SKEL_R_Hand:
                    return "Right Hand";
                case Bones.SKEL_Spine_Root:
                    break;
                case Bones.PH_L_Foot:
                    break;
                case Bones.SKEL_L_Thigh:
                    return "Left Thigh";
                case Bones.FB_L_Brow_Out_000:
                    break;
                case Bones.SKEL_R_Finger00:
                    break;
                case Bones.SKEL_R_Finger10:
                    break;
                case Bones.SKEL_R_Finger20:
                    break;
                case Bones.SKEL_R_Finger30:
                    break;
                case Bones.SKEL_R_Finger40:
                    break;
                case Bones.PH_L_Hand:
                    return "Left Wrist";
                case Bones.RB_L_ForeArmRoll:
                    break;
                case Bones.SKEL_L_Forearm:
                    return "Left Arm";
                case Bones.FB_UpperLip_000:
                    break;
                case Bones.SKEL_L_Calf:
                    return "Left Leg";
                case Bones.SKEL_R_Finger01:
                    break;
                case Bones.SKEL_R_Finger02:
                    break;
                case Bones.SKEL_R_Finger31:
                    break;
                case Bones.SKEL_R_Finger32:
                    break;
                case Bones.SKEL_R_Finger41:
                    break;
                case Bones.SKEL_R_Finger42:
                    break;
                case Bones.SKEL_R_Finger11:
                    break;
                case Bones.SKEL_R_Finger12:
                    break;
                case Bones.SKEL_R_Finger21:
                    break;
                case Bones.SKEL_R_Finger22:
                    break;
                case Bones.SKEL_L_Clavicle:
                    break;
                case Bones.FACIAL_facialRoot:
                    break;
                case Bones.IK_L_Foot:
                    break;
                default:
                    return "Unknown Bone";
            }

            return "Unknown Bone";
        }
    }
}
