using ConsoleScreenHandler.Exceptions;
using ConsoleScreenHandler.Helpers;
using ConsoleScreenHandler.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ConsoleScreenHandler.Handlers;

public sealed class ScreenHandler : IScreenHandler
{
    private readonly ILogger<ScreenHandler> _logger;
    private readonly IHandlerHelpers _handlerHelpers;

    private bool _isFormCompleted;
    private IResult _result;

    public IActionHandler ActionHandler { get; set; } = null!;
    public ConsoleColor BackgroundColor { get; set; }
    public ConsoleColor ForegroundColor { get; set; }

    public Action<IEnumerable<Section>> SectionHandler { get; set; }
    public Func<Section, string> LabelOutput { get; set; }
    public Func<Section, string, bool> AnswerValidation { get; set; }
    public Func<Section, string, string> NotValidAnswerResponse { get; set; }
    public Screen Screen { get; set; } = null!;
    public System.Action ScreenPause { get; set; }
    public Func<string, string> TitleDisplay { get; set; }

    public ScreenHandler(ILogger<ScreenHandler> logger, IHandlerHelpers handlerHelpers, IResult result)
    {
        _logger = logger;
        _handlerHelpers = handlerHelpers;
        _result = result;
        SectionHandler = DefaultSectionHandler();
        LabelOutput = DefaultLabelOutPut();
        AnswerValidation = DefaultAnswerValidation();
        NotValidAnswerResponse = DefaultNotValidAnswerResponse();
        ScreenPause = DefaultScreenPause();
        TitleDisplay = DefaultTitleDisplay();

        ConfigureHelpers();
    }

    public void ShowScreen()
    {
        Console.Title = Screen.Title;
        SetScreenColors();

        SectionHandler(Screen.Sections);
        ActionHandler.ShowActions();

        _isFormCompleted = true;
        Console.WriteLine("Exit code 0.");
    }

    public TEntity GetAnswer<TEntity>()
    {
        if (!_isFormCompleted)
            throw new ConsoleScreenHandlerException("Form is not yet completed");

        var response = JsonConvert.SerializeObject(_result.Data);
        var answer = JsonConvert.DeserializeObject<TEntity>(response);

        if (answer is null)
            throw new ConsoleScreenHandlerException("Answer is not available.");

        return answer;
    }

    private System.Action DefaultScreenPause() => new(() => Console.ReadKey(true));

    private Func<Section, string, string> DefaultNotValidAnswerResponse() => new((section, answer) => "Cannot leave required (*) fields empty.");

    private Func<Section, string, bool> DefaultAnswerValidation() => new((section, answer) => !(section.Required && string.IsNullOrWhiteSpace(answer)));

    private Func<Section, string> DefaultLabelOutPut() => new((section) => $"{(section.Required ? $"{section.Label} *" : section.Label)}{Environment.NewLine}");

    private Action<IEnumerable<Section>> DefaultSectionHandler()
    {
        var defaultSectionHandler = new Action<IEnumerable<Section>>((sections) =>
        {
            foreach (var section in sections)
                do
                {
                    _handlerHelpers.ClearScreen();
                    Console.Write(LabelOutput(section));

                    var answer = Console.ReadLine() ?? string.Empty;

                    if (AnswerValidation(section, answer))
                    {
                        _result.Data.Add(section.Id, answer);
                        break;
                    }

                    _handlerHelpers.ClearScreen();
                    Console.Write(NotValidAnswerResponse(section, answer));
                    _handlerHelpers.Pause();
                }
                while (true);
        });

        return defaultSectionHandler;
    }

    private void ConfigureHelpers()
    {
        _handlerHelpers.ScreenPause = ScreenPause;
        _handlerHelpers.ScreenTitle = Screen.Title;
        _handlerHelpers.TitleDisplay = TitleDisplay;
    }

    private void ShowTitle(Func<string, string> titleDisplay)
    {
        Console.WriteLine(titleDisplay(Screen.Title));
        _logger.LogDebug("Title showed.");
    }

    private Func<string, string> DefaultTitleDisplay()
    {
        var titleDisplay = new Func<string, string>((title) =>
        {
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            var titleDisplay = $"{title}{Environment.NewLine}";
            return titleDisplay;
        });

        return titleDisplay;
    }

    private void SetScreenColors()
    {
        Console.BackgroundColor = BackgroundColor;
        Console.ForegroundColor = ForegroundColor;
    }
}
