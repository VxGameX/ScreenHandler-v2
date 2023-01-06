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
    private Screen _screen = null!;
    private Section _currentSection = null!;
    private Func<Section, string> _labelOutput = null!;
    private Action<IEnumerable<Section>> _sectionHandler = null!;
    private Func<Section, string, bool> _answerValidation = null!;
    private Func<Section, string, string> _notValidAnswerResponse = null!;

    public IResult Result { get; set; }
    public IActionHandler ActionHandler { get; set; } = null!;

    public Action<IEnumerable<Section>> SectionHandler
    {
        get => _sectionHandler;
        set => _sectionHandler = value;
    }

    public Func<Section, string> LabelOutput
    {
        get => _labelOutput;
        set => _labelOutput = value;
    }

    public Func<Section, string, bool> AnswerValidation
    {
        get => _answerValidation;
        set => _answerValidation = value;
    }

    public Func<Section, string, string> NotValidAnswerResponse
    {
        get => _notValidAnswerResponse;
        set => _notValidAnswerResponse = value;
    }

    public Screen Screen
    {
        get => _screen;
        set => _screen = value;
    }

    public ScreenHandler(ILogger<ScreenHandler> logger, IHandlerHelpers handlerHelpers, IResult result)
    {
        _logger = logger;
        _handlerHelpers = handlerHelpers;
        Result = result;
    }

    public void ShowScreen()
    {
        SetTitle();

        _sectionHandler(_screen.Sections);
        ActionHandler.ShowActions();

        _isFormCompleted = true;
        Console.WriteLine("Exit code 0.");
    }

    public TEntity GetAnswer<TEntity>()
    {
        if (!_isFormCompleted)
            throw new ConsoleScreenHandlerException("Form is not yet completed");

        var response = JsonConvert.SerializeObject(Result.Data);
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

    public void ShowSections()
    {
        foreach (var section in _screen.Sections)
        {
            SetCurrentSection(section);
            ShowSection();
        }
    }

    private void SetCurrentSection(Section section) => _currentSection = section;

    private void ShowSection()
    {
        do
        {
            _handlerHelpers.ClearScreen();
            Console.Write(_labelOutput(_currentSection));

            var answer = Console.ReadLine() ?? string.Empty;

            if (_answerValidation(_currentSection, answer))
            {
                Result.Data.Add(_currentSection.Id, answer);
                break;
            }

            _handlerHelpers.ClearScreen();
            Console.Write(_notValidAnswerResponse(_currentSection, answer));
            _handlerHelpers.Pause();
        }
        while (true);
    }
}
