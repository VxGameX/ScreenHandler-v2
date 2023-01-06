using ConsoleScreenHandler.Models;

namespace ConsoleScreenHandler.Handlers;

public interface IScreenHandler
{
    IResult Result { get; set; }
    IActionHandler ActionHandler { get; set; }
    Action<IEnumerable<Section>> SectionHandler { get; set; }
    Func<Section, string> LabelOutput { get; set; }
    Func<Section, string, bool> AnswerValidation { get; set; }
    Func<Section, string, string> NotValidAnswerResponse { get; set; }
    Screen Screen { get; set; }

    TEntity GetAnswer<TEntity>();
    void ShowScreen();
    void ShowSections();
}
