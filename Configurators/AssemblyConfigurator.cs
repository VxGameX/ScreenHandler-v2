using System.Reflection;
using ScreenHandler.Handlers;

namespace ScreenHandler.Configurators;

public static class AssemblyConfigurator
{
    public static void SetExecutingAssembly(Assembly executingAssembly) => ActionHandler.ExecutingAssembly = executingAssembly;
}
