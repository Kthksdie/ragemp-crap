using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serverside.Entities;
using Serverside.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using GTANetworkAPI;

namespace Serverside.Models {
    public class Character : BaseEntity {
        public Character(string accountId) {
            AccountId = accountId;

            Stats = new List<Stat>() {
                new Stat() {
                    Name = "Stamina",
                    Value = 25,
                    Experience = 0
                },
                new Stat() {
                    Name = "Strength",
                    Value = 25,
                    Experience = 0
                },
                new Stat() {
                    Name = "Shooting",
                    Value = 0,
                    Experience = 0
                },
                new Stat() {
                    Name = "Stealth",
                    Value = 0,
                    Experience = 0
                },
                new Stat() {
                    Name = "Lung Capacity",
                    Value = 25,
                    Experience = 0
                },
                new Stat() {
                    Name = "Driving",
                    Value = 25,
                    Experience = 0
                },
                new Stat() {
                    Name = "Flying",
                    Value = 0,
                    Experience = 0
                },
            };

            Skills = new List<Skill>() {
                new Skill() {
                    Name = "Lockpicking",
                    Value = 0,
                    Experience = 0
                },
                new Skill() {
                    Name = "Hotwiring",
                    Value = 0,
                    Experience = 0
                },
                new Skill() {
                    Name = "Fishing",
                    Value = 0,
                    Experience = 0
                },
                new Skill() {
                    Name = "Repairing",
                    Value = 0,
                    Experience = 0
                },
                // Maybe Mining and Lumberjacking?
                // Or maybe a delivery job as a Dump Truck Driver for collecting metals/clay.
                // And a delivery job as a Semi-truck driver (carrying logs) for collecting logs/rope.
                // Resources deliverd can be dropped off for Cash, or stolen off of player.
                //     dropped off resources are made avaliable for sale
                new Skill() {
                    Name = "Gardening", // For growing weed and other drugs.
                    Value = 0,
                    Experience = 0
                },
                new Skill() {
                    Name = "Carpentry", // Crafting props/decorations?
                    Value = 0,
                    Experience = 0
                },
                new Skill() {
                    Name = "Tinkering", // Crafting items (tools, repair kits, body armor?), making drugs?
                    Value = 0,
                    Experience = 0
                },
                new Skill() {
                    Name = "Cooking", // Crafting food/water items
                    Value = 0,
                    Experience = 0
                },
                new Skill() {
                    Name = "Armslore", // Crafting weapons, repairing weapons?
                    Value = 0,
                    Experience = 0
                },
                new Skill() {
                    Name = "Theivery", // Searching home-based inventories, or robbing players
                    Value = 0,
                    Experience = 0
                },
                new Skill() {
                    Name = "Pickpocketing", // Searching player inventories, success only grabs one random item(or cash) from victims inventory?
                    Value = 0,
                    Experience = 0
                }
            };
        }

        [BsonElement("AccountId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AccountId { get; set; }

        [BsonElement("InventoryId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InventoryId { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("MiddleName")]
        public string MiddleName { get; set; }

        [BsonElement("Bio")]
        public string Bio { get; set; }

        [BsonElement("BirthDate")]
        public DateTime BirthDate { get; set; }

        [BsonElement("Model")]
        public uint Model { get; set; } = 0x705E61F2;

        [BsonElement("Health")]
        public int Health { get; set; } = 100;

        [BsonElement("Armor")]
        public int Armor { get; set; } = 0;

        [BsonElement("Hunger")]
        public int Hunger { get; set; } = 100;

        [BsonElement("Thirst")]
        public int Thirst { get; set; } = 100;

        [BsonElement("Stress")]
        public int Stress { get; set; } = 100;

        [BsonElement("Cash")]
        public int Cash { get; set; } = 1000;

        [BsonElement("Bank")]
        public int Bank { get; set; } = 10000;

        [BsonElement("Stats")]
        public List<Stat> Stats { get; set; }

        [BsonElement("Skills")]
        public List<Skill> Skills { get; set; }

        public string FullName {
            get {
                if (!string.IsNullOrEmpty(MiddleName)) {
                    var lastNameInitial = MiddleName.Substring(0, 1).ToUpper();
                    return $"{LastName}, {FirstName} {lastNameInitial}";
                }
                else {
                    return $"{LastName}, {FirstName}";
                }
            }
        }

        public int Age {
            get {
                var age = DateTime.UtcNow.Year - BirthDate.Year;
                if (age > 0) {
                    return age;
                }

                return 1;
            }
        }

        public bool TrySkill(string skillName) {
            var skill = Skills.FirstOrDefault(x => x.Name.ToLower() == skillName.ToLower());

            var random = new Random();

            if (random.Next(0, 100) <= skill.Value) {
                return true;
            }

            return false;
        }

        public void AwardSkillExperience(string skillName, int experience) {
            var skill = Skills.FirstOrDefault(x => x.Name.ToLower() == skillName.ToLower());
            var requiredExperience = skill.Value == 0 ? 100 : skill.Value * 1000;

            if (true) {

            }
        }
    }
}
