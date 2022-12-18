using ScreenHandler.Models;

namespace ScreenHandler.Handlers;

public interface IFormHandler
{
    Form Form { get; set; }
    void Run();
    TEntity GetAnswer<TEntity>();
}
