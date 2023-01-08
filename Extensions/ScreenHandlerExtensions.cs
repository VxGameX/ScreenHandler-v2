using ConsoleScreenHandler.Handlers;
using ConsoleScreenHandler.Helpers;
using ConsoleScreenHandler.Models;
using ConsoleScreenHandler.Options;
using ConsoleScreenHandler.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleScreenHandler.Extensions;

public static class ConsoleScreenHandlerExtensions
{
    public static void AddConsoleScreenHandler(this IServiceCollection services, Action<IConsoleScreenHandlerOptions> options)
    {
        services.AddSingleton<IConsoleScreenHandlerOptions, ConsoleScreenHandlerOptions>();
        services.Configure(options);

        services.AddTransient<IResult, HandlerResponse>();

        services.AddSingleton<IHandlerHelpers, HandlerHelpers>();
        services.AddSingleton<IValidator<Screen>, ScreenValidator>();

        services.AddIActionHandlerFactory();
        services.AddScreenHandlerFactory();
        services.AddIScreenHandlerBuilderFactory();
    }

    private static void AddScreenHandlerFactory(this IServiceCollection services)
    {
        services.AddTransient<ScreenHandler>();
        services.AddSingleton<Func<ScreenHandler>>(sp => () => sp.GetService<ScreenHandler>()!);
        services.AddSingleton<IHandlerFactory<ScreenHandler>, ScreenHandlerFactory>();
    }

    private static void AddIScreenHandlerBuilderFactory(this IServiceCollection services)
    {
        services.AddTransient<IScreenHandlerBuilder, ScreenHandlerBuilder>();
        services.AddSingleton<Func<IScreenHandlerBuilder>>(sp => () => sp.GetService<IScreenHandlerBuilder>()!);
        services.AddSingleton<IHandlerFactory<IScreenHandlerBuilder>, ScreenHandlerBuilderFactory>();
    }

    private static void AddIActionHandlerFactory(this IServiceCollection services)
    {
        services.AddTransient<IActionHandler, ActionHandler>();
        services.AddSingleton<Func<IActionHandler>>(sp => () => sp.GetService<IActionHandler>()!);
        services.AddSingleton<IHandlerFactory<IActionHandler>, ActionHandlerFactory>();
    }
}
