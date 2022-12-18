namespace ScreenHandler.Exceptions;

public class SectionConfigurationException : Exception
{
    public SectionConfigurationException(string message)
        : base($"{message}{(message.LastOrDefault() == '.' ? string.Empty : ".")}{Environment.NewLine}{Environment.NewLine}Exit code: -1")
    {
    }
}
