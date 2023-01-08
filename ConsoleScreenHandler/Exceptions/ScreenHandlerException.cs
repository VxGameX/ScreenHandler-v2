namespace ConsoleScreenHandler.Exceptions;

public class ConsoleScreenHandlerException : Exception
{
    public ConsoleScreenHandlerException(string message)
        : base($"{message}{(message.LastOrDefault() == '.' ? string.Empty : ".")}{Environment.NewLine}{Environment.NewLine}Exit code: -1")
    {
    }
}
