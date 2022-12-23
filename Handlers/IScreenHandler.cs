using ConsoleScreenHandler.Models;

namespace ConsoleScreenHandler.Handlers;

public interface IScreenHandler
{
    Screen Screen { get; set; }
    IActionHandler ActionHandler { get; set; }
    ISectionHandler SectionHandler { get; set; }

    void ShowScreen();
    TEntity GetAnswer<TEntity>();
}
