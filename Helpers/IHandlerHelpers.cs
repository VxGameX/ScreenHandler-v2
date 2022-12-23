namespace ConsoleScreenHandler.Helpers;

public interface IHandlerHelpers
{
    string ScreenTitle { get; set; }

    void ClearScreen();
    void Pause();
}
