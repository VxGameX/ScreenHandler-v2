namespace ConsoleScreenHandler.Helpers;

public interface IHandlerHelpers
{
    void ClearScreen(Func<string, string> titleDisplay, string title);
    void Pause(Action screenPause);
}
