using Microsoft.Extensions.Logging;

namespace ConsoleScreenHandler.Handlers;

public sealed class ActionHandlerFactory : IHandlerFactory<IActionHandler>
{
    private readonly ILogger<ScreenHandlerBuilder> _logger;
    private readonly Func<IActionHandler> _factory;

    public ActionHandlerFactory(ILogger<ScreenHandlerBuilder> logger, Func<IActionHandler> factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public IActionHandler Create() => _factory();
}
