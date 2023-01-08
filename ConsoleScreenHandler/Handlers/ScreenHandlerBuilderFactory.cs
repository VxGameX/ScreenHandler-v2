using Microsoft.Extensions.Logging;

namespace ConsoleScreenHandler.Handlers;

public class ScreenHandlerBuilderFactory : IHandlerFactory<IScreenHandlerBuilder>
{
    private readonly ILogger<ScreenHandlerBuilderFactory> _logger;
    private readonly Func<IScreenHandlerBuilder> _factory;

    public ScreenHandlerBuilderFactory(ILogger<ScreenHandlerBuilderFactory> logger, Func<IScreenHandlerBuilder> factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public IScreenHandlerBuilder Create() => _factory();
}
