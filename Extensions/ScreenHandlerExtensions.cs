using ConsoleScreenHandler.Handlers;
using ConsoleScreenHandler.Helpers;
using ConsoleScreenHandler.Models;
using ConsoleScreenHandler.Options;
using ConsoleScreenHandler.Validators;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace ConsoleScreenHandler.Extensions;

public static class ConsoleScreenHandlerExtensions
{
    public static void AddConsoleScreenHandler(this IServiceCollection services, Action<ConsoleScreenHandlerOptions> options)
    {
        services.Configure(options);

        services.AddHandlerResponse();
        services.AddConsoleScreenHandlerOptions();
        services.AddSingleton<ICollection<Screen>, Collection<Screen>>();

        services.AddHelpers();
        services.AddValidators();

        services.AddScreenHandlerFactory();
        services.AddActionHandlerFactory();
    }

    private static void AddHelpers(this IServiceCollection services) => services.AddSingleton<IHandlerHelpers, HandlerHelpers>();

    private static void AddConsoleScreenHandlerOptions(this IServiceCollection services) => services.AddSingleton<IConsoleScreenHandlerOptions, ConsoleScreenHandlerOptions>();

    private static void AddValidators(this IServiceCollection services) => services.AddSingleton<IValidator<Screen>, ScreenValidator>();

    private static void AddHandlerResponse(this IServiceCollection services)
    {
        services.AddTransient<IDictionary<string, string>>(sp =>
        {
            return new Dictionary<string, string>();
        });
        services.AddTransient<IResult, HandlerResponse>();
    }

    private static void AddScreenHandlerFactory(this IServiceCollection services)
    {
        services.AddTransient<IScreenHandler, ScreenHandler>();
        services.AddSingleton<Func<IScreenHandler>>(x => () => x.GetService<IScreenHandler>()!);
        services.AddSingleton<IScreenHandlerBuilder, ScreenHandlerBuilder>();
    }

    private static void AddActionHandlerFactory(this IServiceCollection services)
    {
        services.AddTransient<IActionHandler, ActionHandler>();
        services.AddSingleton<Func<IActionHandler>>(x => () => x.GetService<IActionHandler>()!);
        services.AddSingleton<IActionHandlerFactory, ActionHandlerFactory>();
    }
}
