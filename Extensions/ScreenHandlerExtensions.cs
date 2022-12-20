using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ScreenHandler.Handlers;

namespace ScreenHandler.Extensions;

public static class ScreenHandlerExtensions
{
    public static IServiceCollection AddCurrentAssembly(this IServiceCollection services, string assembly)
    {
        ActionHandler.ClientAssembly = Assembly.Load(assembly);
        return services;
    }
}
