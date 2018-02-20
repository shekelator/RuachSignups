using MongoDB.Driver;
using System;
using System.Security.Authentication;

namespace RuachSignups.Infrastructure
{
    public class ShabbatRepository
    {
        private readonly MongoClient m_mongoClient;

        public ShabbatRepository(string connectionString)
        {
            var settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            m_mongoClient = new MongoClient(settings);
        }

    }
}
