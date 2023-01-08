using Microsoft.Extensions.Logging;

namespace ConsoleScreenHandler.Helpers;

public class HandlerHelpers : IHandlerHelpers
{
    private readonly ILogger<HandlerHelpers> _logger;

    public HandlerHelpers(ILogger<HandlerHelpers> logger) => _logger = logger;

    public void ClearScreen(Func<string, string> titleDisplay, string title)
    {
        Console.Clear();
        _logger.LogDebug("Console screen cleared.");

        Console.WriteLine(titleDisplay(title));
        _logger.LogDebug("Title showed.");
    }

    public void Pause(Action screenPause)
    {
        screenPause();
        _logger.LogDebug("Screen paused.");
    }
}
