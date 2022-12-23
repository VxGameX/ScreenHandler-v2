using ConsoleScreenHandler.Models;
using Microsoft.Extensions.Logging;

namespace ConsoleScreenHandler.Handlers;

public class SectionHandlerFactory : ISectionHandlerFactory
{
    private readonly ILogger<SectionHandlerFactory> _logger;
    private readonly Func<ISectionHandler> _factory;

    public SectionHandlerFactory(ILogger<SectionHandlerFactory> logger, Func<ISectionHandler> factory)
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
