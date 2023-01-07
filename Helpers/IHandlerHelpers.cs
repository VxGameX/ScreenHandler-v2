namespace ConsoleScreenHandler.Helpers;

public interface IHandlerHelpers
{
    Action ScreenPause { get; set; }
    string ScreenTitle { get; set; }
    Func<string, string> TitleDisplay { get; set; }

    void ClearScreen();
    void Pause();
}
