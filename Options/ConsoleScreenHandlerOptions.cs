using System.Reflection;

namespace ConsoleScreenHandler.Options;

public class ConsoleScreenHandlerOptions
{
    public Assembly ExecutingAssembly { get; set; } = null!;
    public ConsoleColor ForegroundColor { get; set; } = Console.ForegroundColor;
    public ConsoleColor BackgroundColor { get; set; } = Console.BackgroundColor;
    public bool TitleCentralized { get; set; } = false;
}
