using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScreenHandler.Handlers;
using ScreenHandler.Validators;

namespace ScreenHandler.Extensions;

public static class ScreenHandlerExtensions
{
    public static IServiceCollection AddScreenHandlerValidators(this IServiceCollection services)
    {
        services.AddSingleton<ISectionValidator, SectionValidator>();
        services.AddSingleton<IFormValidator, FormValidator>();
        return services;
    }

    public static IServiceCollection AddFormHandlerBuilder<TEntity>(this IServiceCollection services, string formPath)
    {
        services.AddSingleton<IFormHandlerBuilder<TEntity>>(sp =>
        {
            var log = sp.GetService<ILogger<FormHandlerBuilder<TEntity>>>()!;
            var validator = sp.GetService<IFormValidator>()!;
            var formHandlerBuilder = new FormHandlerBuilder<TEntity>(log, validator, formPath);
            return formHandlerBuilder;
        });
        return services;
    }
}
