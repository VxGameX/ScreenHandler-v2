using ConsoleScreenHandler.Exceptions;
using ConsoleScreenHandler.Helpers;
using ConsoleScreenHandler.Models;
using ConsoleScreenHandler.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ConsoleScreenHandler.Handlers;

public sealed class ScreenHandler : IScreenHandler
{
    private readonly ILogger<ScreenHandler> _logger;
    private readonly IHandlerHelpers _handlerHelpers;
    private readonly IOptions<ConsoleScreenHandlerOptions> _options;
    private bool _isFormCompleted;

    public IActionHandler ActionHandler { get; set; } = null!;
    public ISectionHandler SectionHandler { get; set; } = null!;
    public Screen Screen { get; set; } = null!;

    public ScreenHandler(ILogger<ScreenHandler> logger, IHandlerHelpers handlerHelpers, IOptions<ConsoleScreenHandlerOptions> options)
    {
        _logger = logger;
        _handlerHelpers = handlerHelpers;
        _options = options;
    }

    public void ShowScreen()
    {
        SetScreenColors();
        SetTitle();

        SectionHandler.ShowSections();
        ActionHandler.ShowActions();

        _isFormCompleted = true;
        Console.WriteLine("Exit code 0.");
    }

    private void SetScreenColors()
    {
        var options = _options.Value;
        Console.BackgroundColor = options.BackgroundColor;
        Console.ForegroundColor = options.ForegroundColor;
    }

    public TEntity GetAnswer<TEntity>()
    {
        if (!_isFormCompleted)
            throw new ConsoleScreenHandlerException("Form is not yet completed");

        var response = JsonConvert.SerializeObject(SectionHandler.Result.Data);
        var answer = JsonConvert.DeserializeObject<TEntity>(response);

        if (answer is null)
            throw new ConsoleScreenHandlerException("Answer is not available.");

        return answer;
    }

    private void SetTitle()
    {
        Console.Title = Screen.Title;
        _handlerHelpers.ScreenTitle = Screen.Title;
    }
}
