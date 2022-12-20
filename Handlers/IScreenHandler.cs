using ScreenHandler.Models;

namespace ScreenHandler.Handlers;

public interface IScreenHandler
{
    ScreenDefinition Screen { get; set; }
    void Run();
    TEntity GetAnswer<TEntity>();
}
