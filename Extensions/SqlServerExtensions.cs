using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScreenHandler.Entities;
using ScreenHandler.Repositories;
using ScreenHandler.Settings;

namespace ScreenHandler.SqlServer;

public static class SqlServerExtensions
{
    public static IServiceCollection AddSqlServer<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>((sp, options) =>
        {
            var configuration = sp.GetService<IConfiguration>()!;
            var sqlServerSettings = configuration.GetSection(nameof(SqlServerSettings))
                .Get<SqlServerSettings>()!;
            options.UseSqlServer(sqlServerSettings.ConnectionString);
        });

        return services;
    }

    public static IServiceCollection AddSqlRepository<TRepository, TEntity, TIdentifier>(this IServiceCollection services)
        where TRepository : class, IRepository<TEntity, TIdentifier>
        where TEntity : IEntity<TIdentifier>
        where TIdentifier : struct
    {
        services.AddScoped<IRepository<TEntity, TIdentifier>, TRepository>();
        return services;
    }
}
