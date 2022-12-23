using System.Reflection;
using ConsoleScreenHandler.Handlers;

namespace ConsoleScreenHandler.Configurators;

public static class AssemblyConfigurator
{
    public static void SetExecutingAssembly(Assembly executingAssembly) => ExecutingAssembly = executingAssembly;

    internal static Assembly ExecutingAssembly { get; set; } = null!;
}
