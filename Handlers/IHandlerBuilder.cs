namespace ConsoleScreenHandler.Handlers;

public interface IHandlerBuilder<THandler>
    where THandler : class
{
    THandler Build(string screenPath);
}
