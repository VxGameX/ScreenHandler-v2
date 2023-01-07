using System.Reflection;

namespace ConsoleScreenHandler.Options;

public class ConsoleScreenHandlerOptions : IConsoleScreenHandlerOptions
{
    public Assembly ExecutingAssembly { get; set; } = null!;
}
