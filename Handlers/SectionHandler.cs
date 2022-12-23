using ConsoleScreenHandler.Helpers;
using ConsoleScreenHandler.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ConsoleScreenHandler.Handlers;

public class SectionHandler : ISectionHandler
{
    private readonly ILogger<SectionHandler> _logger;
    private readonly IHandlerHelpers _handlerHelpers;
    private Section _currentSection = null!;
    private IResponse _response;

    public IEnumerable<Section> Sections { get; set; } = null!;

    public SectionHandler(ILogger<SectionHandler> logger, IHandlerHelpers handlerHelpers, IResponse response)
    {
        _logger = logger;
        _handlerHelpers = handlerHelpers;
        _response = response;
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
                _response.Data.Add(_currentSection.Id, answer);
                break;
            }

            _handlerHelpers.ClearScreen();
            Console.Write("Cannot leave required (*) sections empty.");
            _handlerHelpers.Pause();
        }
        while (true);
    }

    private void ShowLabel() => Console.WriteLine($"{_currentSection.Label} {(_currentSection.Required ? "*" : string.Empty)}{Environment.NewLine}");

    private bool IsValidAnswer(string answer) => !(_currentSection.Required && string.IsNullOrWhiteSpace(answer));
}
