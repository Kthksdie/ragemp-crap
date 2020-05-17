using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Serverside.Interfaces {
    public interface IEntity {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }

        [BsonElement("Created")]
        DateTime Created { get; set; }

        [BsonElement("Modified")]
        DateTime Modified { get; set; }
    }
}
