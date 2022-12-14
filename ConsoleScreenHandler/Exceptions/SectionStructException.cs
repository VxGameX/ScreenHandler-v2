namespace ConsoleScreenHandler.Exceptions;

public class SectionStructException : Exception
{
    public SectionStructException(string message)
        : base($"{message}{(message.LastOrDefault() == '.' ? string.Empty : ".")}{Environment.NewLine}{Environment.NewLine}Exit code: -1")
    {
    }
}

