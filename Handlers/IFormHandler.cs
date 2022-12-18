using ScreenHandler.Models;

namespace ScreenHandler.Handlers;

public interface IFormHandler<TEntity>
{
    Form Form { get; set; }
    void Run();
    TEntity GetAnswer();
}
