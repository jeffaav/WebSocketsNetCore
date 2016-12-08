using MongoDB.Driver;
using WebSocketsNetCore.Models.Repositories;

namespace WebSocketsNetCore.Models
{
    public class UnitOfWork
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public UnitOfWork(MongoClient client)
        {
            this._client = client;
            this._database = client.GetDatabase("Websockets");
        }
        

        private TopicRepository _topicRepository;
        public TopicRepository TopicRepository
        {
            get
            {
                if (_topicRepository == null) 
                    _topicRepository = new TopicRepository(_database);
                return _topicRepository;
            }
        }
    }
}