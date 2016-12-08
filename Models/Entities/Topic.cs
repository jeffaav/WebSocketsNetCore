using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebSocketsNetCore.Models.Entities
{
    public class Topic
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}