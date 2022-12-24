using ConsoleScreenHandler.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleScreenHandler.Helpers;

public class HandlerHelpers : IHandlerHelpers
{
    private readonly ILogger<HandlerHelpers> _logger;
    private readonly IOptions<ConsoleScreenHandlerOptions> _options;

    public string ScreenTitle { get; set; } = null!;

    public HandlerHelpers(ILogger<HandlerHelpers> logger, IOptions<ConsoleScreenHandlerOptions> options)
    {
        _logger = logger;
        _options = options;
    }

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
        var options = _options.Value;

        if (options.TitleCentralized)
            Console.SetCursorPosition((Console.WindowWidth - ScreenTitle.Length) / 2, Console.CursorTop);

        Console.WriteLine($"{ScreenTitle}{Environment.NewLine}");
        _logger.LogDebug("Title showed.");
    }

    public void Pause()
    {
        Console.ReadKey(true);
        _logger.LogDebug("Screen paused.");
    }
}
