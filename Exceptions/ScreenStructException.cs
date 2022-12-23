namespace ConsoleScreenHandler.Exceptions;

public class ScreenStructException : Exception
{
    public ScreenStructException(string message)
        : base($"{message}{(message.LastOrDefault() == '.' ? string.Empty : ".")}{Environment.NewLine}{Environment.NewLine}Exit code: -1")
    {
    }
}
