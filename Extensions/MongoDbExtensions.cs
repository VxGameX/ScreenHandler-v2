using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using ScreenHandler.Models;
using ScreenHandler.Repositories;
using ScreenHandler.Settings;

namespace ScreenHandler.MongoDB;

public static class MongoDbExtensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));

        services.AddSingleton(sp =>
        {
            var configuration = sp.GetService<IConfiguration>();

            var serviceSettings = configuration!
                .GetSection(nameof(ServiceSettings))
                .Get<ServiceSettings>();

            var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings))
                .Get<MongoDbSettings>();

            var mongoDbClient = new MongoClient(mongoDbSettings.ConnectionString);
            return mongoDbClient.GetDatabase(serviceSettings.ServiceName);
        });

        return services;
    }

    public static IServiceCollection AddMongoRepository<TEntity, TIdentifier>(this IServiceCollection services, string collectionName)
        where TEntity : Entity<TIdentifier>
        where TIdentifier : struct
    {
        services.AddSingleton<IRepository<TEntity, TIdentifier>>(sp =>
        {
            var database = sp.GetService<IMongoDatabase>();
            return new MongoRepository<TEntity, TIdentifier>(database!, collectionName);
        });

        return services;
    }
}
