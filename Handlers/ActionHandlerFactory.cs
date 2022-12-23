using Microsoft.Extensions.Logging;

namespace ConsoleScreenHandler.Handlers;

public sealed class ActionHandlerFactory : IActionHandlerFactory
{
    private readonly ILogger<ScreenHandlerFactory> _logger;
    private readonly Func<IActionHandler> _factory;

    public ActionHandlerFactory(ILogger<ScreenHandlerFactory> logger, Func<IActionHandler> factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public IActionHandler Create(IEnumerable<Models.Action> actions)
    {
        var newActionHandler = _factory();
        newActionHandler.Actions = actions;
        return newActionHandler;
    }
}
