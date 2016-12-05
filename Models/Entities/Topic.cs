using MongoDB.Bson;

namespace WebSocketsNetCore.Models.Entities
{
    public class Topic
    {
        public ObjectId _id { get; set; }

        public string name { get; set; }
    }
}