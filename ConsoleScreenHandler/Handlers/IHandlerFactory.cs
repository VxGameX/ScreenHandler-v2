namespace ConsoleScreenHandler.Handlers;

public interface IHandlerFactory<THandler>
    where THandler : class
{
    THandler Create();
}