using Microsoft.Extensions.Logging;

namespace ConsoleScreenHandler.Handlers;

public class ScreenHandlerFactory : IHandlerFactory<ScreenHandler>
{
    private readonly ILogger<ScreenHandlerFactory> _logger;
    private readonly Func<ScreenHandler> _factory;

    public ScreenHandlerFactory(ILogger<ScreenHandlerFactory> logger, Func<ScreenHandler> factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public ScreenHandler Create() => _factory();
}
