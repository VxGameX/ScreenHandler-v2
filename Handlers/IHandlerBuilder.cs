namespace ScreenHandler.Handlers;

public interface IHandlerBuilder<THandler>
    where THandler : class
{
    THandler Build();
}
