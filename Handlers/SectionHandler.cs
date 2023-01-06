using ConsoleScreenHandler.Helpers;
using ConsoleScreenHandler.Models;
using ConsoleScreenHandler.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleScreenHandler.Handlers;

public class SectionHandler : ISectionHandler
{
    private readonly ILogger<SectionHandler> _logger;
    private readonly IHandlerHelpers _handlerHelpers;
    private Section _currentSection = null!;
    public IResult Result { get; set; }

    private Func<Section, string> _requiredMarkFunc;
    public Func<Section, string> PromtHandler
    {
        get { return _requiredMarkFunc; }
        set { _requiredMarkFunc = value; }
    }

    public IEnumerable<Section> Sections { get; set; } = null!;

    public SectionHandler(ILogger<SectionHandler> logger, IHandlerHelpers handlerHelpers, IResult response)
    {
        _logger = logger;
        _handlerHelpers = handlerHelpers;
        Result = response;
    }

    public void ShowSections()
    {
        foreach (var section in Sections)
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
            ShowLabel();

            Console.Write(">> ");
            var answer = Console.ReadLine() ?? string.Empty;

            if (IsValidAnswer(answer))
            {
                Result.Data.Add(_currentSection.Id, answer);
                break;
            }

            _handlerHelpers.ClearScreen();
            Console.Write("Cannot leave required (*) sections empty.");
            _handlerHelpers.Pause();
        }
        while (true);
    }

    private void ShowLabel()
    {
        Console.WriteLine(_requiredMarkFunc(this._currentSection));
    }

    private bool IsValidAnswer(string answer) => !(_currentSection.Required && string.IsNullOrWhiteSpace(answer));
}
