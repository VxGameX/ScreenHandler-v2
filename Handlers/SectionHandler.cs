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
    private readonly IOptions<ConsoleScreenHandlerOptions> _options;
    private Section _currentSection = null!;
    public IResult Result { get; set; }

    public IEnumerable<Section> Sections { get; set; } = null!;

    public SectionHandler(ILogger<SectionHandler> logger, IHandlerHelpers handlerHelpers, IResult response, IOptions<ConsoleScreenHandlerOptions> options)
    {
        _logger = logger;
        _handlerHelpers = handlerHelpers;
        Result = response;
        _options = options;
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
        var options = _options.Value;
        switch (options.RequiredMark)
        {
            case RequiredMark.UpperCase:
                Console.WriteLine($"{(_currentSection.Required ? _currentSection.Label.ToUpper() : _currentSection.Label)}{Environment.NewLine}");
                break;
            case RequiredMark.Star:
                Console.WriteLine($"{(_currentSection.Required ? $"{_currentSection.Label} *" : _currentSection.Label)}{Environment.NewLine}");
                break;
        };
    }

    private bool IsValidAnswer(string answer) => !(_currentSection.Required && string.IsNullOrWhiteSpace(answer));
}
