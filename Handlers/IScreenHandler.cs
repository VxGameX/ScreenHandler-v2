using ConsoleScreenHandler.Models;

namespace ConsoleScreenHandler.Handlers;

public interface IScreenHandler
{
    IActionHandler ActionHandler { get; set; }
    Action<IEnumerable<Section>> SectionHandler { set; }
    Func<Section, string> LabelOutput { set; }
    Func<Section, string, bool> AnswerValidation { set; }
    Func<Section, string, string> NotValidAnswerResponse { set; }
    Screen Screen { set; }
    ConsoleColor BackgroundColor { set; }
    ConsoleColor ForegroundColor { set; }
    System.Action ScreenPause { set; }
    Func<string, string> TitleDisplay { set; }

    TEntity GetAnswer<TEntity>();
    void ShowScreen();
}
