using System.Reflection;

namespace ConsoleScreenHandler.Options;

public interface IConsoleScreenHandlerOptions
{
    Assembly ExecutingAssembly { get; set; }
    ConsoleColor ForegroundColor { get; set; }
    ConsoleColor BackgroundColor { get; set; }
    ConsoleColor RequiredMarkColor { get; set; }
    bool TitleCentralized { get; set; }
    RequiredMark RequiredMark { get; set; }
}