using Microsoft.Extensions.Logging;

namespace ConsoleScreenHandler.Helpers;

public class HandlerHelpers : IHandlerHelpers
{
    private readonly ILogger<HandlerHelpers> _logger;

    public Action ScreenPause { get; set; } = null!;
    public string ScreenTitle { get; set; } = null!;
    public Func<string, string> TitleDisplay { get; set; } = null!;

    public HandlerHelpers(ILogger<HandlerHelpers> logger) => _logger = logger;

    public void ClearScreen()
    {
        Console.Clear();
        _logger.LogDebug("Console screen cleared.");

        Console.WriteLine(TitleDisplay(ScreenTitle));
        _logger.LogDebug("Title showed.");
    }

    public void Pause()
    {
        ScreenPause();
        _logger.LogDebug("Screen paused.");
    }
}
