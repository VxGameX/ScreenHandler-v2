using System.Reflection;
using ScreenHandler.Handlers;

namespace ScreenHandler.Configurators;

public static class AssemblyConfigurator
{
    public static void SetExecutingAssembly(Assembly executingAssembly) => ExecutingAssembly = executingAssembly;

    internal static Assembly ExecutingAssembly { get; set; } = null!;
}
