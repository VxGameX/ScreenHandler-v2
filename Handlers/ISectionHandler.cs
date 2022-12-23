using ConsoleScreenHandler.Models;

namespace ConsoleScreenHandler.Handlers;

public interface ISectionHandler
{
    IResult Result { get; set; }
    IEnumerable<Section> Sections { get; set; }

    void ShowSections();
}
