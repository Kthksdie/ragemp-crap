using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Testing.Entities;
using Testing.Extensions;
using Testing.Helpers;

namespace Testing {
    class Program {
        private static string _currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static string _dataDirectory = _currentDirectory + @"\Data";
        private static string _logsDirectory = _currentDirectory + @"\Logs";
        private static string _outputDirectory = _currentDirectory + @"\Output";

        private static Dictionary<int, string> _vehicleColors = new Dictionary<int, string>() {
            { 0, "Metallic Black" },
            { 1, "Metallic Graphite Black" },
            { 2, "Metallic Black Steal" },
            { 3, "Metallic Dark Silver" },
            { 4, "Metallic Silver" },
            { 5, "Metallic Blue Silver" },
            { 6, "Metallic Steel Gray" },
            { 7, "Metallic Shadow Silver" },
            { 8, "Metallic Stone Silver" },
            { 9, "Metallic Midnight Silver" },
            { 10, "Metallic Gun Metal" },
            { 11, "Metallic Anthracite Grey" },
            { 12, "Matte Black" },
            { 13, "Matte Gray" },
            { 14, "Matte Light Grey" },
            { 15, "Util Black" },
            { 16, "Util Black Poly" },
            { 17, "Util Dark Silver" },
            { 18, "Util Silver" },
            { 19, "Util Gun Metal" },
            { 20, "Util Shadow Silver" },
            { 21, "Worn Black" },
            { 22, "Worn Graphite" },
            { 23, "Worn Silver Grey" },
            { 24, "Worn Silver" },
            { 25, "Worn Blue Silver" },
            { 26, "Worn Shadow Silver" },
            { 27, "Metallic Red" },
            { 28, "Metallic Torino Red" },
            { 29, "Metallic Formula Red" },
            { 30, "Metallic Blaze Red" },
            { 31, "Metallic Graceful Red" },
            { 32, "Metallic Garnet Red" },
            { 33, "Metallic Desert Red" },
            { 34, "Metallic Cabernet Red" },
            { 35, "Metallic Candy Red" },
            { 36, "Metallic Sunrise Orange" },
            { 37, "Metallic Classic Gold" },
            { 38, "Metallic Orange" },
            { 39, "Matte Red" },
            { 40, "Matte Dark Red" },
            { 41, "Matte Orange" },
            { 42, "Matte Yellow" },
            { 43, "Util Red" },
            { 44, "Util Bright Red" },
            { 45, "Util Garnet Red" },
            { 46, "Worn Red" },
            { 47, "Worn Golden Red" },
            { 48, "Worn Dark Red" },
            { 49, "Metallic Dark Green" },
            { 50, "Metallic Racing Green" },
            { 51, "Metallic Sea Green" },
            { 52, "Metallic Olive Green" },
            { 53, "Metallic Green" },
            { 54, "Metallic Gasoline Blue Green" },
            { 55, "Matte Lime Green" },
            { 56, "Util Dark Green" },
            { 57, "Util Green" },
            { 58, "Worn Dark Green" },
            { 59, "Worn Green" },
            { 60, "Worn Sea Wash" },
            { 61, "Metallic Midnight Blue" },
            { 62, "Metallic Dark Blue" },
            { 63, "Metallic Saxony Blue" },
            { 64, "Metallic Blue" },
            { 65, "Metallic Mariner Blue" },
            { 66, "Metallic Harbor Blue" },
            { 67, "Metallic Diamond Blue" },
            { 68, "Metallic Surf Blue" },
            { 69, "Metallic Nautical Blue" },
            { 70, "Metallic Bright Blue" },
            { 71, "Metallic Purple Blue" },
            { 72, "Metallic Spinnaker Blue" },
            { 73, "Metallic Ultra Blue" },
            { 74, "Metallic Bright Blue 2" },
            { 75, "Util Dark Blue" },
            { 76, "Util Midnight Blue" },
            { 77, "Util Blue" },
            { 78, "Util Sea Foam Blue" },
            { 79, "Util Lightning blue" },
            { 80, "Util Maui Blue Poly" },
            { 81, "Util Bright Blue" },
            { 82, "Matte Dark Blue" },
            { 83, "Matte Blue" },
            { 84, "Matte Midnight Blue" },
            { 85, "Worn Dark blue" },
            { 86, "Worn Blue" },
            { 87, "Worn Light blue" },
            { 88, "Metallic Taxi Yellow" },
            { 89, "Metallic Race Yellow" },
            { 90, "Metallic Bronze" },
            { 91, "Metallic Yellow Bird" },
            { 92, "Metallic Lime" },
            { 93, "Metallic Champagne" },
            { 94, "Metallic Pueblo Beige" },
            { 95, "Metallic Dark Ivory" },
            { 96, "Metallic Choco Brown" },
            { 97, "Metallic Golden Brown" },
            { 98, "Metallic Light Brown" },
            { 99, "Metallic Straw Beige" },
            { 100, "Metallic Moss Brown" },
            { 101, "Metallic Biston Brown" },
            { 102, "Metallic Beechwood" },
            { 103, "Metallic Dark Beechwood" },
            { 104, "Metallic Choco Orange" },
            { 105, "Metallic Beach Sand" },
            { 106, "Metallic Sun Bleeched Sand" },
            { 107, "Metallic Cream" },
            { 108, "Util Brown" },
            { 109, "Util Medium Brown" },
            { 110, "Util Light Brown" },
            { 111, "Metallic White" },
            { 112, "Metallic Frost White" },
            { 113, "Worn Honey Beige" },
            { 114, "Worn Brown" },
            { 115, "Worn Dark Brown" },
            { 116, "Worn Straw Beige" },
            { 117, "Brushed Steel" },
            { 118, "Brushed Black steel" },
            { 119, "Brushed Aluminium" },
            { 120, "Chrome" },
            { 121, "Worn Off White" },
            { 122, "Util Off White" },
            { 123, "Worn Orange" },
            { 124, "Worn Light Orange" },
            { 125, "Metallic Securicor Green" },
            { 126, "Worn Taxi Yellow" },
            { 127, "Police Car Blue" },
            { 128, "Matte Green" },
            { 129, "Matte Brown" },
            { 130, "Worn Orange 2" },
            { 131, "Matte White" },
            { 132, "Worn White" },
            { 133, "Worn Olive Army Green" },
            { 134, "Pure White" },
            { 135, "Hot Pink" },
            { 136, "Salmon Pink" },
            { 137, "Metallic Vermillion Pink" },
            { 138, "Orange" },
            { 139, "Green" },
            { 140, "Blue" },
            { 141, "Mettalic Black Blue" },
            { 142, "Metallic Black Purple" },
            { 143, "Metallic Black Red" },
            { 144, "Hunter Green" },
            { 145, "Metallic Purple" },
            { 146, "Metaillic V Dark Blue" },
            { 147, "MODSHOP BLACK1" },
            { 148, "Matte Purple" },
            { 149, "Matte Dark Purple" },
            { 150, "Metallic Lava Red" },
            { 151, "Matte Forest Green" },
            { 152, "Matte Olive Drab" },
            { 153, "Matte Desert Brown" },
            { 154, "Matte Desert Tan" },
            { 155, "Matte Foilage Green" },
            { 156, "Default Alloy" },
            { 157, "Epsilon Blue" },
            { 158, "Pure Gold" },
            { 159, "Brushed Gold" }
        };

        private static Dictionary<int, string> _vehicleColors2 = new Dictionary<int, string>() {
            { 127, "Police Car Blue" },
            { 134, "Pure White" },
            { 135, "Hot Pink" },
            { 136, "Salmon Pink" },
            { 138, "Orange" },
            { 139, "Green" },
            { 140, "Blue" },
            { 144, "Hunter Green" },
            { 157, "Epsilon Blue" }
        };

        private static Dictionary<int, string> _vehicleColors3 = new Dictionary<int, string>() {
            { 117, "Brushed Steel" },
            { 118, "Brushed Black steel" },
            { 119, "Brushed Aluminium" },
            { 159, "Brushed Gold" },
            { 158, "Pure Gold" },
            { 156, "Default Alloy" },
            { 120, "Chrome" },
        };

        static void Main(string[] args) {
            Logging.Log($"Starting...");

            //SetupAnimations();
            //SetupSynchedAnimations();
            SetupBuildings();

            Logging.Log($"Finished.");
            Console.ReadKey();
        }

        public static void SetupBuildings() {
            var buildings = new List<Building>() {
                new Building() {
                    Id = 0,
                    Type = Enums.BuildingTypes.Motel,
                    Interior = new Vector3(152.0097f, -1005.757f, -98.5f),
                    Exit = new Vector3(151.4046f, -1007.882f, -99f),
                    Wardrobe = new Vector3(151.7827f, -1001.386f, -99f)
                }
            };

            var houses = new List<House>() {
                new House() {
                    Id = 0,
                    Name = "Motel Room",
                    Entrance = new Vector3(313.1987f, -225.3823f, 54.22116f),
                    Building = 0
                }
            };

            Utilities.WriteToJsonFile($@"{_dataDirectory}\Buildings.json", buildings);
            Utilities.WriteToJsonFile($@"{_dataDirectory}\Houses.json", houses);

            Logging.Log($"        Buildings: {buildings.Count}");
            Logging.Log($"        Houses: {houses.Count}");
            Logging.Log($"    Finished.");
        }

        public static void SetupAnimations() {
            Logging.Log($"    Reading 'Animations.txt'...");

            var animations = new List<Animation>();

            var animationsFile = new StreamReader($@"{_dataDirectory}\Animations.txt");
            string line;
            while ((line = animationsFile.ReadLine()) != null) {
                var lineValues = line.ToList(' ');

                var animation = new Animation() {
                    Index = Convert.ToInt32(lineValues[0]),
                    Library = lineValues[1],
                    Name = lineValues[2],
                    Duration = Convert.ToInt32(lineValues[3])
                };

                animations.Add(animation);

                //Logging.Log($"        Index: {animation.Index}");
                //Logging.Log($"            Library:  {animation.Library}");
                //Logging.Log($"            Name:     {animation.Name}");
                //Logging.Log($"            Duration: {animation.Duration}");
            }

            animationsFile.Close();

            Utilities.WriteToJsonFile($@"{_dataDirectory}\Animations.json", animations);

            Logging.Log($"        Animations: {animations.Count}");
            Logging.Log($"    Finished.");
        }

        public static void SetupSynchedAnimations() {
            Logging.Log($"    Reading 'SynchedAnimations.xml'...");

            var path = $@"{_dataDirectory}\SynchedAnimations.xml";
            var fileLines = string.Join(" ", File.ReadAllLines(path));

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(fileLines);

            string jsonString = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
            var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);

            var synchedAnimations = jsonObject["SynchedAnimations"];
            var synchedAnims = synchedAnimations["SynchedAnim"].ToObject<JArray>();

            var synchedScenes = new List<AnimationSet>();

            foreach (var synchedAnim in synchedAnims) {
                var synchedScene = new AnimationSet() {
                    Index = synchedScenes.Count,
                    Title = synchedAnim["@title"]?.ToString(),
                    Category = synchedAnim["@category"]?.ToString(),
                    ActorsAligned = Convert.ToBoolean(synchedAnim["@actorsAligned"]?.ToString()),
                    DeltaZ = Convert.ToSingle(synchedAnim["@deltaZ"]?.ToString())
                };

                //Logging.Log($"        {synchedScene.Title}");

                if (synchedAnim["ActorAnim"] != null) {
                    var actorAnims = new List<int>();

                    try {
                        var actorAnim = synchedAnim["ActorAnim"]?.ToObject<JArray>();
                        if (actorAnim != null) {
                            for (int i = 0; i < actorAnim.Count; i++) {
                                var anim = actorAnim[i];
                                var animIndex = Convert.ToInt32(anim["@animIndex"].ToString());

                                actorAnims.Add(animIndex);
                            }
                        }
                    }
                    catch {
                        var anim = synchedAnim["ActorAnim"];
                        var animIndex = Convert.ToInt32(anim["@animIndex"].ToString());

                        actorAnims.Add(animIndex);
                    }

                    synchedScene.ActorAnims.AddRange(actorAnims);
                }

                if (synchedAnim["ObjectAnim"] != null && synchedAnim["Object"] != null) {
                    var objectAnims = new List<int>();

                    try {
                        var objectAnim = synchedAnim["ObjectAnim"]?.ToObject<JArray>();
                        if (objectAnim != null) {
                            for (int i = 0; i < objectAnim.Count; i++) {
                                var anim = objectAnim[i];
                                var animIndex = Convert.ToInt32(anim["@animIndex"].ToString());

                                objectAnims.Add(animIndex);
                            }
                        }
                    }
                    catch {
                        objectAnims.Add(Convert.ToInt32(synchedAnim["ObjectAnim"]["@animIndex"].ToString()));
                    }

                    synchedScene.ObjectAnims.AddRange(objectAnims);
                }

                if (synchedAnim["Object"] != null) {
                    var objects = new List<string>();

                    try {
                        var objectAnim = synchedAnim["Object"]?.ToObject<JArray>();
                        if (objectAnim != null) {
                            for (int i = 0; i < objectAnim.Count; i++) {
                                var anim = objectAnim[i];
                                var propName = anim["@propName"].ToString();

                                objects.Add(propName);
                            }
                        }
                    }
                    catch {
                        objects.Add(synchedAnim["Object"]["@propName"].ToString());
                    }

                    synchedScene.Objects.AddRange(objects);
                }

                synchedScenes.Add(synchedScene);
            }

            Utilities.WriteToJsonFile($@"{_dataDirectory}\AnimationSets.json", synchedScenes);

            Logging.Log($"        AnimationSets: {synchedScenes.Count}");
            Logging.Log($"    Finished.");
        }
    }
}
