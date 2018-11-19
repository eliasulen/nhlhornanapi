using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhlhornanapi.Database.Entity;

namespace nhlhornanapi.Database.Context
{
    public class MatchContext
    {
        private readonly IMongoDatabase _database = null;

        public MatchContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Match> Matches
        {
            get
            {
                return _database.GetCollection<Match>("Matches");
            }
        }
    }
}
