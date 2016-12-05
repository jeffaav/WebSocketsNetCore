using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebSocketsNetCore.Models.Repositories
{
    public class GenericRepository<T>
    {
        protected readonly MongoClient client;
        protected readonly IMongoDatabase database;
        protected readonly IMongoCollection<T> collection;

        public GenericRepository(MongoClient client)
        {
            this.client = client; 
            this.database = client.GetDatabase("Websockets");
            this.collection = this.database.GetCollection<T>(typeof(T).Name); 
            Console.WriteLine(typeof(T).Name);
        }

        public IList<T> Find(FilterDefinition<T> filter) 
        {
            return this.collection.Find(filter).ToList();
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
            return this.FindOne(new BsonDocument 
            {
                { "_id", objectId }
            });
        }
    }
}