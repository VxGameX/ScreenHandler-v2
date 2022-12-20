using ScreenHandler.Helpers;
using ScreenHandler.Models;

namespace ScreenHandler.Handlers;

public class SectionHandler : IHandler
{
    private Section _currentSection = null!;
    private readonly IEnumerable<Section> _sections;
    private readonly string _screenTitle;

    public SectionHandler(ScreenHandler handler)
    {
        _sections = handler.Sections;
        _screenTitle = handler.Title;
    }

    public void Run()
    {
        foreach (var section in _sections)
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
            HandlerHelpers.ClearScreen();
            ShowLabel();

            Console.Write(">> ");
            var answer = Console.ReadLine() ?? string.Empty;

            if (IsValidAnswer(answer))
                break;

            HandlerHelpers.ClearScreen();
            Console.Write("Cannot leave required (*) sections empty.");
            HandlerHelpers.Pause();
        }
        while (true);
    }

    private void ShowLabel() => Console.WriteLine($"{_currentSection.Label} {(_currentSection.Required ? "*" : string.Empty)}{Environment.NewLine}");

    private bool IsValidAnswer(string answer) => !(_currentSection.Required && string.IsNullOrWhiteSpace(answer));
}
