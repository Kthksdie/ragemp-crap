using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serverside.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Serverside.Models {
    public abstract class BaseEntity : IEntity {
        public BaseEntity() {
            Created = DateTime.UtcNow;
            Modified = Created;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Created")]
        public DateTime Created { get; set; }

        [BsonElement("Modified")]
        public DateTime Modified { get; set; }
    }
}
