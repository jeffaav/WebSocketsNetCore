using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebSocketsNetCore.Models.Repositories
{
    public class GenericRepository<T>
    {
        protected readonly IMongoDatabase _database;
        protected readonly IMongoCollection<T> _collection;

        public GenericRepository(IMongoDatabase database)
        {
            this._database = database;
            this._collection = database.GetCollection<T>(typeof(T).Name); 
        }

        public IList<T> Find(FilterDefinition<T> filter) 
        {
            return this._collection.Find(filter).ToList();
        }

        public IList<T> Find()
        {
            return this.Find(new BsonDocument());
        }  

        public T FindOne(FilterDefinition<T> filter)
        {
            return this.Find(filter).FirstOrDefault();
        }

        public T FindOne(string objectId)
        {
            return this.FindOne(getFilterId(objectId));
        }

        public Task<DeleteResult> DeleteOne(string objectId)
        {
            return this._collection.DeleteOneAsync(getFilterId(objectId));
        }

        public Task InsertOne(T document)
        {
            return this._collection.InsertOneAsync(document);
        }

        public Task<ReplaceOneResult> UpdateOne(string objectId, T document)
        {
            return this._collection.ReplaceOneAsync(getFilterId(objectId), document);
        }

        protected Func<string, BsonDocument> getFilterId = (objectId) => new BsonDocument 
        { 
            { "_id", ObjectId.Parse(objectId) } 
        };
    }
}