namespace ScreenHandler.Handlers;

public interface IScreenHandlerBuilder
{
    IScreenHandler Build();
    ScreenHandlerBuilder RegisterMultipleScreens(string path);
    ScreenHandlerBuilder RegisterSingleScreen(string path);
    ScreenHandlerBuilder SetEntryPoint(string screenId);
}
