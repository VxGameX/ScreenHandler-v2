using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScreenHandler.Configurators;
using ScreenHandler.Handlers;
using ScreenHandler.Validators;

namespace ScreenHandler.Extensions;

public static class FormExtensions
{
    public static IServiceCollection AddFormHandlerBuilder(this IServiceCollection services, string formPath)
    {
        services.AddSingleton<IFormValidator, FormValidator>();
        services.AddSingleton<IFormHandlerBuilder>(sp =>
        {
            var log = sp.GetService<ILogger<FormHandlerBuilder>>()!;
            var validator = sp.GetService<IFormValidator>()!;
            var configurator = sp.GetService<ISectionConfigurator>()!;
            var formHandlerBuilder = new FormHandlerBuilder(log, validator, configurator, formPath);
            return formHandlerBuilder;
        });
        return services;
    }
}
