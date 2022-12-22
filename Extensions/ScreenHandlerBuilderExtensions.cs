using Microsoft.Extensions.DependencyInjection;
using ConsoleScreenHandler.Handlers;
using ConsoleScreenHandler.Validators;

namespace ConsoleScreenHandler.Extensions;

public static class ConsoleScreenHandlerBuilderExtensions
{
    public static IServiceCollection AddConsoleScreenHandler(this IServiceCollection services)
    {
        services.AddTransient<IDictionary<string, string>, Dictionary<string, string>>();
        services.AddSingleton<IScreenValidator, ScreenValidator>();
        services.AddSingleton<IHandlerBuilder<ScreenHandler>, ScreenHandlerBuilder>();
        return services;
    }
}
