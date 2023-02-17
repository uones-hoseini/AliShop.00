using AliShop.Application.Contexts.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliShop.Persistence.Contexts.MongoContext
{
    public class MongoDbContext<T> : Application.Contexts.Interfaces.IMongoDbContext<T>
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<T> mongoCollection;
        public MongoDbContext()
        {
            var Client=new MongoClient();
            db = Client.GetDatabase("Visitor00Db");
            mongoCollection = db.GetCollection<T>(typeof(T).Name);
        }
        public IMongoCollection<T> GetCollection()
        {
           return mongoCollection;
        }
    }
}
