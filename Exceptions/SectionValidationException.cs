namespace ConsoleScreenHandler.Exceptions;

public class SectionValidationException : Exception
{
    public SectionValidationException(string message)
        : base($"{message}{(message.LastOrDefault() == '.' ? string.Empty : ".")}{Environment.NewLine}{Environment.NewLine}Exit code: -1")
    {
    }
}
