namespace ScreenHandler.Exceptions;

public class ScreenHandlerException : Exception
{
    public ScreenHandlerException(string message)
        : base($"{message}{(message.LastOrDefault() == '.' ? string.Empty : ".")}{Environment.NewLine}{Environment.NewLine}Exit code: -1")
    {
    }
}
