using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using ScreenHandler.Entities;
using ScreenHandler.Repositories;
using ScreenHandler.Settings;

namespace ScreenHandler.Extensions;

/// <summary>
/// Manages MongoDB repositories.
/// </summary>
public static class MongoDbExtensions
{
    /// <summary>
    /// Adds IMongoDatabase to the Dependency Injection IServiceCollection.
    /// </summary>
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
                .Get<ServiceSettings>()!;

            var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings))
                .Get<MongoDbSettings>()!;

            var mongoDbClient = new MongoClient(mongoDbSettings.ConnectionString);
            return mongoDbClient.GetDatabase(serviceSettings.ServiceName);
        });

        return services;
    }

    /// <summary>
    /// Adds an IRepository to the Dependency Injection IServiceCollection.
    /// </summary>
    /// <typeparam name="TEntity">Entity type of the repository.</typeparam>
    /// <param name="collectionName">The name of the collection in the database.</param>
    public static IServiceCollection AddMongoRepository<TEntity>(this IServiceCollection services, string collectionName)
        where TEntity : IEntity
    {
        services.AddSingleton<IRepository<TEntity>>(sp =>
        {
            var database = sp.GetService<IMongoDatabase>();
            return new MongoRepository<TEntity>(database!, collectionName);
        });

        return services;
    }
}
