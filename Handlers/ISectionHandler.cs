using ConsoleScreenHandler.Models;

namespace ConsoleScreenHandler.Handlers;

public interface ISectionHandler
{
    IEnumerable<Section> Sections { get; set; }

    void ShowSections();
}
