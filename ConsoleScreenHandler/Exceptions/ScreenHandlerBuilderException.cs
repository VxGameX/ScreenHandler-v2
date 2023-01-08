namespace ConsoleScreenHandler.Exceptions;

public class ScreenHandlerBuilderException : Exception
{
    public ScreenHandlerBuilderException(string message)
        : base($"{message}{(message.LastOrDefault() == '.' ? string.Empty : ".")}{Environment.NewLine}{Environment.NewLine}Exit code: -1")
    {
    }
}
