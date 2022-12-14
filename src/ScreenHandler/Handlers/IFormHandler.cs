using ScreenHandler.Settings;

namespace ScreenHandler.Handlers;

public interface IFormHandler
{
    public TEntity GetAnswer<TEntity>();
    void Run();
}
