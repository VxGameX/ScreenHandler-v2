using System.Reflection;

namespace ConsoleScreenHandler.Options;

public interface IConsoleScreenHandlerOptions
{
    Assembly ExecutingAssembly { get; set; }
}
