namespace ScreenHandler.Exceptions;

internal class FormHandlerException : Exception
{
    public FormHandlerException(string message)
        : base($"{message}{(message.LastOrDefault() == '.' ? string.Empty : ".")}{Environment.NewLine}{Environment.NewLine}Exit code: -1")
    {
    }
}