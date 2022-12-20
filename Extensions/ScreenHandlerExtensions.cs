using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ScreenHandler.Handlers;

namespace ScreenHandler.Extensions;

public static class ScreenHandlerExtensions
{
    public static IServiceCollection AddExecutingAssembly(this IServiceCollection services, Assembly executingAssembly)
    {
        ActionHandler.ExecutingAssembly = executingAssembly;
        return services;
    }
}
