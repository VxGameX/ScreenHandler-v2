namespace ConsoleScreenHandler.Handlers;

public interface IScreenHandler
{
    TEntity GetAnswer<TEntity>();
    void ShowScreen();
}
