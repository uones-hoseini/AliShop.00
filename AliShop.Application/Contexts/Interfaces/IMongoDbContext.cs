using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliShop.Application.Contexts.Interfaces
{
    public interface IMongoDbContext<T>
    {
        public IMongoCollection<T> GetCollection();
    }
}
