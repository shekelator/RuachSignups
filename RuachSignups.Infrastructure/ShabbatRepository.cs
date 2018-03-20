using MongoDB.Driver;
using System;
using System.Security.Authentication;
using Signups.Core;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using Microsoft.FSharp.Core;

namespace RuachSignups.Infrastructure
{
    public class ShabbatRepository
    {
        private readonly MongoClient m_mongoClient;
        private const string DbName = "signups";
        private const string CollectionName = "shabbats";

        public ShabbatRepository(string connectionString)
        {
            var settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            m_mongoClient = new MongoClient(settings);
        }

        public async Task CreateAsync(Shabbat.Occasion shabbat)
        {
            IMongoCollection<ShabbatDto> collection = GetDbCollection();
            await collection.InsertOneAsync(new ShabbatDto(shabbat));
        }

        private IMongoCollection<ShabbatDto> GetDbCollection()
        {
            var db = m_mongoClient.GetDatabase(DbName);
            var collection = db.GetCollection<ShabbatDto>(CollectionName);
            return collection;
        }

        public async Task UpsertMany(IEnumerable<Shabbat.Occasion> newShabbats)
        {
            var collection = GetDbCollection();
            var shabbats = await collection.Find(new BsonDocument()).ToListAsync();
            //var shabbatsByDate = new Dictionary<DateTime, Shabbat.Occasion>();
            var shabbatsByDate = shabbats.ToDictionary(s => s.Date, s => s);

            foreach(var s in newShabbats)
            {
                if (!shabbatsByDate.ContainsKey(s.Date))
                {
                    await collection.InsertOneAsync(new ShabbatDto(s));
                }
            }
        }

        private class ShabbatDto
        {
            public ShabbatDto(Shabbat.Occasion shabbat)
            {
                if (FSharpOption<string>.get_IsSome(shabbat.Id))
                {
                    Id = new ObjectId(shabbat.Id.Value);
                }
                Date = shabbat.Date;
                Title = shabbat.Title;
                Openings = shabbat.Openings.Select(o => new Opening(o)).ToList();
            }
            public ObjectId Id { get; set; }
            public DateTime Date { get; set; }
            public string Title { get; set; }
            public List<Opening> Openings { get; set; }
        }
        private class Opening
        {
            public Opening(Shabbat.Opening opening)
            {
                Title = opening.Title;
                Category = opening.Category;
            }
            public string Title { get; set; }
            public Shabbat.OpeningCategory Category { get; set; }
        }
    }
}
