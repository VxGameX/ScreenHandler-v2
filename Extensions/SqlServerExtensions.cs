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

    public static IServiceCollection AddSqlRepository<TRepository, TEntity>(this IServiceCollection services)
        where TRepository : class, IRepository<TEntity>
        where TEntity : IEntity
    {
        services.AddScoped<IRepository<TEntity>, TRepository>();
        return services;
    }
}
