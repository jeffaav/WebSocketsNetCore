using MongoDB.Driver;
using WebSocketsNetCore.Models.Entities;

namespace WebSocketsNetCore.Models.Repositories
{
    public class TopicRepository : GenericRepository<Topic>
    {
        public TopicRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}