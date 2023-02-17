using AliShop.Application.Contexts.Interfaces;
using AliShop.Domain.Visitors;
using MongoDB.Driver;
using System;
using System.Linq;

namespace AliShop.Application.Visitors.VisitorOnLine
{
    public class VisitorOnLineService : IVisitorOnLineService
    {
        private readonly IMongoDbContext<OnLineVisitor> mongoDbContext;
        private readonly IMongoCollection<OnLineVisitor> mongoCollection;

        public VisitorOnLineService(IMongoDbContext<OnLineVisitor> mongoDbContext)
        {
            this.mongoDbContext = mongoDbContext;
            mongoCollection = mongoDbContext.GetCollection();
        }

        public void ConnectUser(string ClientId)
        {
            mongoCollection.InsertOne(new OnLineVisitor
            {
                Time = DateTime.Now,
                ClientId = ClientId,
            });
        }

        public void DisConnectUser(string ClientId)
        {
            mongoCollection.FindOneAndDelete(p=>p.ClientId== ClientId);
        }

        public int GetCount()
        {
            return mongoCollection.AsQueryable().Count();
        }
    }
}
