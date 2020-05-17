using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Serverside.Models;
using MongoDB.Driver;

namespace Serverside.Services {
    // Take files from NUGET (lib\netstandard1.5):
    //     MongoDB.Bson.dll
    //     MongoDB.Driver.dll
    //     MongoDB.Driver.Core.dll
    // Copy files to: RAGEMP\server-files\bridge\runtime

    public class StorageDB<T> where T : BaseEntity {
        private readonly string _host = "mongodb://localhost:27017";
        private readonly string _databaseName = "Zuluhotel";
        private readonly IMongoCollection<T> _storageCollection;

        public StorageDB() {
            var client = new MongoClient(_host);
            var database = client.GetDatabase(_databaseName);

            var collectionName = $"{typeof(T).Name}";

            _storageCollection = database.GetCollection<T>(collectionName);

            if (_storageCollection == null) {
                database.CreateCollection(collectionName);

                _storageCollection = database.GetCollection<T>(collectionName);
            }
        }

        public List<T> Get() {
            return _storageCollection.Find(entity => true).ToList();
        }

        public T Get(string id) {
            return _storageCollection.Find<T>(entity => entity.Id == id).FirstOrDefault();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) {
            return _storageCollection.Find<T>(predicate).ToEnumerable();
        }

        public T Create(T entity) {
            _storageCollection.InsertOne(entity);

            return entity;
        }

        public T Update(T entity) {
            entity.Modified = DateTime.UtcNow;

            _storageCollection.ReplaceOne(x => x.Id == entity.Id, entity);

            return entity;
        }

        public void Remove(T entity) {
            _storageCollection.DeleteOne(x => x.Id == entity.Id);
        }
    }
}
