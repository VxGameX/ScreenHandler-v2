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
            {
                ShowTitle();
                ShowSection(section);
            }
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

    private void ShowSection(Section section)
    {
        do
        {
            ClearScreen();
            // TODO: Set required mark to RED
            ShowLabel(section);
            // Checks if section has options
            if (section.Input.Options is null)
            {
                Console.Write("\n>> ");
                // TODO: Add TEXT, INT and FLOAT validations according to input type
                var answer = Console.ReadLine();

                if (IsValidAnswer(section, answer))
                    break;

                ClearScreen();

                // TODO: Create AnswerValidator
                Console.Write("Cannot leave required (*) fields empty.");
                Pause();
                continue;
            }
            ShowOptions(section);
            break;
        }
        while (true);
    }

    private static void SelectOption(Input input)
    {
        if (!int.TryParse(Console.ReadLine(), out int selectedOption))
            throw new NotImplementedException("Could not parse selected option");

        if (input.SelectedOptions is null)
            input.SelectedOptions = new List<int>();

        input.SelectedOptions.Add(selectedOption);
    }

    private void ShowOptions(Section section)
    {
        var counter = 1;
        switch (section.Input.Type)
        {
            case "radiobutton":
                counter = 1;
                foreach (var option in section.Input.Options!)
                {
                    var selectedOption = 0;
                    if (section.Input.SelectedOptions is not null)
                        selectedOption = section.Input.SelectedOptions.FirstOrDefault();

                    Console.WriteLine($"{counter++} - ({(selectedOption != (section.Input.Options.IndexOf(option) + 1) ? string.Empty : "X")}) {option}");
                }
                break;
            case "checkbox":
                do
                {
                    counter = 1;
                    ClearScreen();
                    foreach (var option in section.Input.Options!)
                    {
                        var selectedOption = 0;
                        if (section.Input.SelectedOptions is not null)
                            selectedOption = section.Input.SelectedOptions.FirstOrDefault(op => op == (section.Input.Options.IndexOf(option) + 1));

                        Console.WriteLine($"{counter++} - [{(selectedOption is 0 ? " " : "X")}] {option}");
                    }

                    if (!section.Required)
                        Console.WriteLine("\n* Press any key to select another option or Enter to continue to the next section");

                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                        break;

                    Console.Write(">> ");
                    SelectOption(section.Input);
                } while (true);
                break;
            default:
                break;
        }
    }

    private static void ShowLabel(Section section) => Console.WriteLine($"{section.Label} {(section.Required ? "*" : string.Empty)}");

    private static void Pause() => Console.ReadKey();

    // TODO: Export function to IFormAnswerValidator
    private static bool IsValidAnswer(Section section, string? answer) => !(string.IsNullOrWhiteSpace(answer) && section.Required);

    private static void CentralizeTitle(string title)
    {
        Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
        Console.WriteLine(title);
    }

    private static void SetTitle(Title title) => Console.Title = title.Label;

    private void ShowTitle()
    {
        if (_form.Title.Centralized)
        {
            CentralizeTitle(_form.Title.Label);
            return;
        }
        ShowTitle(_form.Title.Label);
    }

    private static void ShowTitle(string title) => Console.WriteLine(title);

    private void ClearScreen()
    {
        Console.Clear();
        ShowTitle();
    }
}
