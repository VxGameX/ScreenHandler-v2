namespace ConsoleScreenHandler.Handlers;

public interface IScreenHandlerFactory
{
    IScreenHandler Create(string screenPath);
}
