using System;
using System.Collections.Generic;
using System.Text;
using Serverside.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Serverside.Models {
    public class Account : BaseEntity {
        public Account() {
            IPs = new List<IPAddress>();
        }

        [BsonElement("SocialClubName")]
        public string SocialClubName { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("Online")]
        public bool Online { get; set; }

        [BsonElement("Whitelisted")]
        public bool Whitelisted { get; set; } // Maybe Whitelisted people get put into their own Dimension?

        // TODO: Tracking of Logged-in duration

        [BsonElement("IPs")]
        public List<IPAddress> IPs { get; set; }
    }
}
