namespace ScreenHandler.Exceptions;

internal class FormStructException : Exception
{
    public FormStructException(string message)
        : base($"{message}{(message.LastOrDefault() == '.' ? string.Empty : ".")}{Environment.NewLine}{Environment.NewLine}Exit code: -1")
    {
    }
}
