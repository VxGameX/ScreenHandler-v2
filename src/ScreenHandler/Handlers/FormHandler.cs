using ScreenHandler.Settings;

namespace ScreenHandler.Handlers;

public sealed class FormHandler : IFormHandler
{
    private ConfigFile _form;
    private Section _currentSection;

    public FormHandler(ConfigFile form, IEnumerable<Section> sections)
    {
        _form = form;
        _form.Sections = sections;
    }

    public static IFormHandlerBuilder CreateBuilder(string formPath) => new FormHandlerBuilder(formPath);

    public IFormHandler ClearAnswers()
    {
        throw new NotImplementedException();
    }

    public IFormHandler NextSection()
    {
        throw new NotImplementedException();
    }

    public IFormHandler PreviousSection()
    {
        throw new NotImplementedException();
    }

    public IFormHandler Save()
    {
        throw new NotImplementedException();
    }

    public void Run()
    {
        SetTitle(_form.Title);

        try
        {
            // IsFormValid(_form);

            foreach (var section in _form.Sections!)
                ShowSection(section);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public IFormHandler ReviewAnswers()
    {
        throw new NotImplementedException();
    }

    public IFormHandler RestartForm()
    {
        throw new NotImplementedException();
    }

    private void SetCurrentSection(Section section) => _currentSection = section;

    private static void ShowSection(Section section)
    {
        do
        {
            ClearScreen(section.Label);
            // TODO: Set required mark to RED
            ShowLabel(section);
            // Checks if section has options
            if (section.Input.Options is not null)
                ShowOptions(section);

            Console.Write("\n>> ");
            // TODO: Add TEXT, INT and FLOAT validations according to input type
            var answer = Console.ReadLine();

            if (IsValidAnswer(section, answer))
                break;

            ClearScreen(section.Label);

            // TODO: Export to IFormAnswerValidator
            Console.Write("Cannot leave required (*) fields empty.");
            Pause();
        }
        while (true);
    }

    private static void ShowOptions(Section section)
    {
        switch (section.Input.Type)
        {
            case "bullet":
                foreach (var option in section.Input.Options!)
                {
                    var selectedOption = 0;
                    if (section.Input.SelectedOptions is not null)
                        selectedOption = section.Input.SelectedOptions.FirstOrDefault();

                    Console.WriteLine($"({(selectedOption != (option.IndexOf(option) + 1) ? string.Empty : "X")}) {option}");
                }
                break;
            case "checkbox":
                foreach (var option in section.Input.Options!)
                {
                    var selectedOption = 0;
                    if (section.Input.SelectedOptions is not null)
                        selectedOption = section.Input.SelectedOptions.FirstOrDefault(op => op == (option.IndexOf(option) + 1));

                    Console.WriteLine($"[{(selectedOption is 0 ? string.Empty : "X")}] {option}");
                }
                break;
            default:
                break;
        }
    }

    private static void ShowLabel(Section section) => Console.Write($"{section.Label} {(section.Required ? "*" : string.Empty)}\n>> ");

    private static void Pause() => Console.ReadKey();

    // TODO: Export function to IFormAnswerValidator
    private static bool IsValidAnswer(Section field, string? answer) => !(string.IsNullOrWhiteSpace(answer) && field.Required);

    private static void CentralizeTitle(string title)
    {
        Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
        Console.WriteLine(title);
    }

    private static void SetTitle(Title title) => Console.Title = title.Label;

    private static void ShowTitle(Title title)
    {
        if (title.Centralized)
        {
            CentralizeTitle(title.Label);
            return;
        }
        ShowTitle(title.Label);
    }

    private static void ShowTitle(string title) => Console.WriteLine(title);

    private static void ClearScreen(string sectionLabel)
    {
    }
}
