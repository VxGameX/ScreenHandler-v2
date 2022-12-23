using ConsoleScreenHandler.Models;

namespace ConsoleScreenHandler.Handlers;

public interface ISectionHandlerFactory
{
    ISectionHandler Create(IEnumerable<Section> sections);
}
