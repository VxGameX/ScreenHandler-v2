using ConsoleScreenHandler.Models;
using Microsoft.Extensions.Logging;

namespace ConsoleScreenHandler.Handlers;

public class SectionHandlerFactory : ISectionHandlerFactory
{
    private readonly ILogger<SectionHandlerFactory> _logger;
    private readonly Func<SectionHandler> _factory;

    public SectionHandlerFactory(ILogger<SectionHandlerFactory> logger, Func<SectionHandler> factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public ISectionHandler Create(IEnumerable<Section> sections)
    {
        var newSectionHandler = _factory();
        newSectionHandler.Sections = sections;
        return newSectionHandler;
    }
}
