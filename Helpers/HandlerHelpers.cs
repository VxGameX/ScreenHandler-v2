using Microsoft.Extensions.Logging;

namespace ConsoleScreenHandler.Helpers;

public class HandlerHelpers : IHandlerHelpers
{
    private readonly ILogger<HandlerHelpers> _logger;

    public string ScreenTitle { get; set; } = null!;

    public HandlerHelpers(ILogger<HandlerHelpers> logger) => _logger = logger;

    public void ClearScreen()
    {
        Console.ResetColor();
        _logger.LogDebug("Console colors reseted.");
        Console.Clear();
        _logger.LogDebug("Console screen cleared.");
        ShowTitle();
    }

    private void ShowTitle()
    {
        Console.WriteLine($"{ScreenTitle}{Environment.NewLine}");
        _logger.LogDebug("Title showed.");
    }

    public void Pause()
    {
        Console.ReadKey(true);
        _logger.LogDebug("Screen paused.");
    }
}
