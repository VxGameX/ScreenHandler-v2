using ScreenHandler.Settings;

namespace ScreenHandler.Handlers;

public sealed class FormHandler : IFormHandler
{
    public ConfigFile Form { get; set; }
    public Section CurrentSection { get; set; }

    public FormHandler(FormHandlerBuilder builder)
    {
        Form = builder.Form;
        Form.Sections = builder.FormSections;
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
        SetTitle();
        try
        {
            foreach (var section in Form.Sections!)
            {
                SetCurrentSection(section);
                ShowSection();
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

    private void SetCurrentSection(Section section) => CurrentSection = section;

    private void ShowSection()
    {
        do
        {
            ClearScreen();
            // TODO: Set required mark to RED
            ShowLabel();
            // Checks if section has options
            if (CurrentSection.Input.Options is null)
            {
                Console.Write(">> ");
                // TODO: Add TEXT, INT and FLOAT validations according to input type
                var answer = Console.ReadLine() ?? string.Empty;

                if (IsValidAnswer(answer))
                    break;

                ClearScreen();

                Console.Write("Cannot leave required (*) sections empty.");
                Pause();
                continue;
            }
            ShowOptions();
            break;
        }
        while (true);
    }

    private bool SelectOption(Input input, bool required, int counter)
    {
        if (input.SelectedOptions is null)
            input.SelectedOptions = new List<int>();

        Console.Write("\n>> ");
        var option = Console.ReadLine()?
            .ToLower()
            .Trim() ?? string.Empty;

        if (!IsOptionValid(input, required, counter, option, out int selectedOption))
            return false;

        if (selectedOption == 0)
            return true;

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

        if (input.Type == "checkbox")
        {
            var checkboxOption = input.SelectedOptions!.FirstOrDefault(so => so == selectedOption);
            if (checkboxOption == selectedOption)
            {
                input.SelectedOptions!.Remove(checkboxOption);
                return false;
            }
        }

        input.SelectedOptions!.Add(selectedOption);
        return false;
    }

    private bool IsOptionValid(Input input, bool required, int counter, string option, out int selectedOption)
    {
        if (!int.TryParse(option, out selectedOption))
        {
            switch (option)
            {
                case "":
                    if (required && input.SelectedOptions?.FirstOrDefault() == 0)
                    {
                        ClearScreen();
                        Console.Write("Cannot leave required (*) sections empty.");
                        Pause();
                        return false;
                    }
                    return true;
                default:
                    ClearScreen();
                    Console.Write($"You must select a valid option (1-{counter - 1}) or press Enter to go to the next section.");
                    Pause();
                    return false;
            }
        }

        if (selectedOption < 1 || selectedOption > input.Options!.Count)
        {
            ClearScreen();
            Console.Write($"You must select a valid option (1-{counter - 1}) or press Enter to go to the next section.");
            Pause();
            return false;
        }

        return true;
    }

    private void ShowOptions()
    {
        var counter = 1;
        switch (CurrentSection.Input.Type)
        {
            case "radiobutton":
                do
                {
                    ClearScreen();
                    // TODO: Set required mark to RED
                    ShowLabel();

                    counter = 1;
                    foreach (var option in CurrentSection.Input.Options!)
                    {
                        var selectedOption = 0;
                        if (CurrentSection.Input.SelectedOptions is not null)
                            selectedOption = CurrentSection.Input.SelectedOptions.FirstOrDefault();

                        Console.WriteLine($"{counter++} - ({(selectedOption != (CurrentSection.Input.Options.IndexOf(option) + 1) ? " " : "X")}) {option}");
                    }

                    if (SelectOption(CurrentSection.Input, CurrentSection.Required, counter))
                        break;
                } while (true);
                break;
            case "checkbox":
                do
                {
                    ClearScreen();
                    // TODO: Set required mark to RED
                    ShowLabel();

                    counter = 1;
                    foreach (var option in CurrentSection.Input.Options!)
                    {
                        var selectedOption = 0;
                        if (CurrentSection.Input.SelectedOptions is not null)
                            selectedOption = CurrentSection.Input.SelectedOptions.FirstOrDefault(op => op == (CurrentSection.Input.Options.IndexOf(option) + 1));

                        Console.WriteLine($"{counter++} - [{(selectedOption is 0 ? " " : "X")}] {option}");
                    }

                    if (SelectOption(CurrentSection.Input, CurrentSection.Required, counter))
                        break;
                } while (true);
                break;
            default:
                break;
        }
    }

    private void ShowLabel() => Console.WriteLine($"{CurrentSection.Label} {(CurrentSection.Required ? "*" : string.Empty)}\n");

    private static void Pause() => Console.ReadKey();

    private bool IsValidAnswer(string answer) => !(CurrentSection.Required && string.IsNullOrWhiteSpace(answer));

    private void CentralizeTitle()
    {
        Console.SetCursorPosition((Console.WindowWidth - Form.Title.Label.Length) / 2, Console.CursorTop);
        Console.WriteLine(Form.Title.Label);
    }

    private void SetTitle() => Console.Title = Form.Title.Label;

    private void ShowTitle()
    {
        if (Form.Title.Centralized)
        {
            CentralizeTitle();
            return;
        }
        SetTitleColors();
        Console.WriteLine($"{Form.Title.Label}\n");
        SetBodyColors();
    }

    private void ClearScreen()
    {
        Console.Clear();
        ShowTitle();
    }

    private void SetBodyColors()
    {
        Console.ForegroundColor = SetColor(Form.Title.ForegroundColor);
        Console.BackgroundColor = SetColor(Form.Title.BackgroundColor);
    }

    private void SetTitleColors()
    {
        Console.ForegroundColor = SetColor(Form.Title.ForegroundColor);
        Console.BackgroundColor = SetColor(Form.Title.BackgroundColor);
    }

    private ConsoleColor SetColor(string color) => color switch
    {
        "black" => ConsoleColor.Black,
        "blue" => ConsoleColor.Blue,
        "cyan" => ConsoleColor.Cyan,
        "darkBlue" => ConsoleColor.DarkBlue,
        "darkCyan" => ConsoleColor.DarkCyan,
        "darkGray" => ConsoleColor.DarkGray,
        "darkGreen" => ConsoleColor.DarkGreen,
        "darkMagenta" => ConsoleColor.DarkMagenta,
        "darkRed" => ConsoleColor.DarkRed,
        "darkYello" => ConsoleColor.DarkYellow,
        "gray" => ConsoleColor.Gray,
        "green" => ConsoleColor.Green,
        "magenta" => ConsoleColor.Magenta,
        "red" => ConsoleColor.Red,
        "white" => ConsoleColor.White,
        "yellow" => ConsoleColor.Yellow,
        _ => throw new Exception("You must select a valid console color.")
    };
}
