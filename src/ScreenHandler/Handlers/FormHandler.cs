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
                SetCurrentSection(section);
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
                Console.Write(">> ");
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

    private bool SelectOption(Input input, bool required, int counter)
    {
        if (input.SelectedOptions is null)
            input.SelectedOptions = new List<int>();

        if (!required || input.SelectedOptions?.FirstOrDefault() != 0)
            Console.WriteLine("\n* (N)ext section");

        Console.Write("\n>> ");
        var option = Console.ReadLine()?
            .ToLower()
            .Trim() ?? string.Empty;

        return IsOptionValid(input, required, counter, option);
    }

    private bool IsOptionValid(Input input, bool required, int counter, string option)
    {
        if (!int.TryParse(option, out int selectedOption))
        {
            switch (option)
            {
                case "n":
                    if (required && input.SelectedOptions?.FirstOrDefault() == 0)
                    {
                        ClearScreen();
                        Console.Write("Cannot leave required (*) sections empty.");
                        Pause();
                        return false;
                    }
                    return true;
                case "":
                    ClearScreen();
                    Console.Write("To continue you must press N when prompted.");
                    Pause();
                    return false;
                default:
                    ClearScreen();
                    Console.Write($"You must select a valid option (1-{counter - 1}) or press N if allowed.");
                    Pause();
                    return false;
            }
        }

        if (selectedOption < 1 || selectedOption > input.Options!.Count)
        {
            ClearScreen();
            Console.Write($"You must select a valid option (1-{counter - 1}) or press N if allowed.");
            Pause();
            return false;
        }

        if (input.Type == "radiobutton")
        {
            var radioOption = input.SelectedOptions!.FirstOrDefault();
            if (radioOption == selectedOption)
            {
                input.SelectedOptions!.Remove(radioOption);
                return false;
            }

            if (radioOption != 0)
                input.SelectedOptions!.Remove(radioOption);
        }
        input.SelectedOptions!.Add(selectedOption);
        return false;
    }

    private void ShowOptions(Section section)
    {
        var counter = 1;
        switch (section.Input.Type)
        {
            case "radiobutton":
                do
                {
                    ClearScreen();
                    // TODO: Set required mark to RED
                    ShowLabel(section);

                    counter = 1;
                    foreach (var option in section.Input.Options!)
                    {
                        var selectedOption = 0;
                        if (section.Input.SelectedOptions is not null)
                            selectedOption = section.Input.SelectedOptions.FirstOrDefault();

                        Console.WriteLine($"{counter++} - ({(selectedOption != (section.Input.Options.IndexOf(option) + 1) ? " " : "X")}) {option}");
                    }

                    if (SelectOption(section.Input, section.Required, counter))
                        break;
                } while (true);
                break;
            case "checkbox":
                do
                {
                    ClearScreen();
                    // TODO: Set required mark to RED
                    ShowLabel(section);

                    counter = 1;
                    ClearScreen();
                    foreach (var option in section.Input.Options!)
                    {
                        var selectedOption = 0;
                        if (section.Input.SelectedOptions is not null)
                            selectedOption = section.Input.SelectedOptions.FirstOrDefault(op => op == (section.Input.Options.IndexOf(option) + 1));

                        Console.WriteLine($"{counter++} - [{(selectedOption is 0 ? " " : "X")}] {option}");
                    }

                    SelectOption(section.Input, section.Required, counter);
                } while (true);
            default:
                break;
        }
    }

    private static void ShowLabel(Section section) => Console.WriteLine($"{section.Label} {(section.Required ? "*" : string.Empty)}\n");

    private static void Pause() => Console.ReadKey();

    // TODO: Export function to IFormAnswerValidator
    private static bool IsValidAnswer(Section section, string? answer)
    {
        if (section.Required && string.IsNullOrWhiteSpace(answer))
            return false;

        return true;
    }

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
