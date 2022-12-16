namespace ScreenHandler.Exceptions;

internal class FormHandlerBuilderException : Exception
{
    public FormHandlerBuilderException(string message)
        : base($"{message}{(message.LastOrDefault() == '.' ? string.Empty : ".")}{Environment.NewLine}{Environment.NewLine}Exit code: -1")
    {
    }
}
