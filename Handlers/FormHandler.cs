using Newtonsoft.Json;
using ScreenHandler.Exceptions;
using ScreenHandler.Models;

namespace ScreenHandler.Handlers;

public sealed class FormHandler<TEntity> : IFormHandler<TEntity>
{
    private Section _currentSection = null!;
    private bool _isFormCompleted;
    private string _answer;

    public Form Form { get; set; }

    public FormHandler(IFormHandlerBuilder<TEntity> builder)
    {
        Form = builder.Form;
    }

    public void Run()
    {
        ClearScreen();
        SetTitle();
        if (!string.IsNullOrWhiteSpace(Form.Description))
            ShowDescription();

        foreach (var section in Form.Sections!)
        {
            SetCurrentSection(section);
            ShowSection();
        }

        _isFormCompleted = true;
        Console.WriteLine("Exit code 0");
    }

    public TEntity GetAnswer()
    {
        if (!_isFormCompleted)
            throw new FormHandlerException("Form is not yet completed");

        var answer = JsonConvert.DeserializeObject<TEntity>(_answer);

        if (answer is null)
            throw new FormHandlerException("Answer is not available.");

        return answer;
    }

    private void ShowDescription()
    {
        Console.Write(Form.Description);
        Pause();
    }

    private void SetCurrentSection(Section section) => _currentSection = section;

    private void ShowSection()
    {
        do
        {
            ClearScreen();
            ShowLabel();
            if (_currentSection.Input.Type is "checkbox" or "radiobutton")
            {
                ShowOptions();
                break;
            }

            Console.Write(">> ");
            var answer = Console.ReadLine() ?? string.Empty;

            if (IsValidAnswer(answer, out string message))
                break;

            ClearScreen();
            Console.Write(message);
            Pause();
            continue;
        }
        while (true);
    }

    private bool SelectOption(Input input, bool required, int counter)
    {
        if (input.SelectedOptions is null)
            input.SelectedOptions = new List<int>();

        Console.Write($"{Environment.NewLine}>> ");
        var option = Console.ReadLine()?
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
                input.SelectedOptions.Remove(radioOption);
                return false;
            }

            if (radioOption != 0)
                input.SelectedOptions.Remove(radioOption);
        }

        if (input.Type == "checkbox")
        {
            var checkboxOption = input.SelectedOptions!.FirstOrDefault(so => so == selectedOption);
            if (checkboxOption == selectedOption)
            {
                input.SelectedOptions.Remove(checkboxOption);
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
        switch (_currentSection.Input.Type)
        {
            case "radiobutton":
                do
                {
                    ClearScreen();
                    // TODO: Set required mark to RED
                    ShowLabel();

                    counter = 1;
                    foreach (var option in _currentSection.Input.Options!)
                    {
                        var selectedOption = 0;
                        if (_currentSection.Input.SelectedOptions is not null)
                            selectedOption = _currentSection.Input.SelectedOptions.FirstOrDefault();

                        Console.WriteLine($"{counter++} - ({(selectedOption != (_currentSection.Input.Options.IndexOf(option) + 1) ? " " : "X")}) {option}");
                    }

                    if (SelectOption(_currentSection.Input, _currentSection.Required, counter))
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
                    foreach (var option in _currentSection.Input.Options!)
                    {
                        var selectedOption = 0;
                        if (_currentSection.Input.SelectedOptions is not null)
                            selectedOption = _currentSection.Input.SelectedOptions.FirstOrDefault(op => op == (_currentSection.Input.Options.IndexOf(option) + 1));

                        Console.WriteLine($"{counter++} - [{(selectedOption is 0 ? " " : "X")}] {option}");
                    }

                    if (SelectOption(_currentSection.Input, _currentSection.Required, counter))
                        break;
                } while (true);
                break;
            default:
                break;
        }
    }

    private void ShowLabel() => Console.WriteLine($"{_currentSection.Label} {(_currentSection.Required ? "*" : string.Empty)}{Environment.NewLine}");

    private static void Pause() => Console.ReadKey(true);

    private bool IsValidAnswer(string answer, out string message)
    {
        if (_currentSection.Required && string.IsNullOrWhiteSpace(answer))
        {
            message = "Cannot leave required (*) sections empty.";
            return false;
        }

        if (_currentSection.Input.Type == "int" && !int.TryParse(answer, out int _))
        {
            message = "Answer must be an integer number.";
            return false;
        }

        if (_currentSection.Input.Type == "float" && !double.TryParse(answer, out double _))
        {
            message = "Answer must be a floating point number.";
            return false;
        }

        _currentSection.Input.Answer = answer;
        message = string.Empty;
        return true;
    }

    private void CentralizeTitle()
    {
        Console.SetCursorPosition((Console.WindowWidth - Form.Title.Label.Length) / 2, Console.CursorTop);
        Console.WriteLine($"{Form.Title.Label}{Environment.NewLine}");
    }

    private void SetTitle() => Console.Title = Form.Title.Label;

    private void ShowTitle()
    {
        if (Form.Title.Centralized)
        {
            SetTitleColors();
            CentralizeTitle();
            if (Form.Body is not null)
                SetBodyColors();
            return;
        }

        SetTitleColors();
        Console.WriteLine($"{Form.Title.Label}{Environment.NewLine}");
        if (Form.Body is not null)
            SetBodyColors();
    }

    private void ClearScreen()
    {
        Console.ResetColor();
        Console.Clear();
        ShowTitle();
    }

    private void SetBodyColors()
    {
        if (Form.Body.ForegroundColor is not null)
            Console.ForegroundColor = GetColor(Form.Body.ForegroundColor);

        if (Form.Body.BackgroundColor is not null)
            Console.BackgroundColor = GetColor(Form.Body.BackgroundColor);
    }

    private void SetTitleColors()
    {
        if (Form.Title.ForegroundColor is not null)
            Console.ForegroundColor = GetColor(Form.Title.ForegroundColor);

        if (Form.Title.BackgroundColor is not null)
            Console.BackgroundColor = GetColor(Form.Title.BackgroundColor);
    }

    private ConsoleColor GetColor(string color)
    {
        const string black = "black", blue = "blue", cyan = "cyan",
            darkBlue = "darkBlue", darkCyan = "darkCyan", darkGray = "darkGray",
            darkGreen = "darkGreen", darkMagenta = "darkMagenta", darkRed = "darkRed",
            darkYellow = "darkYello", gray = "gray", green = "green",
            mangenta = "magenta", red = "red", white = "white", yellow = "yellow";

        return color switch
        {
            black => ConsoleColor.Black,
            blue => ConsoleColor.Blue,
            cyan => ConsoleColor.Cyan,
            darkBlue => ConsoleColor.DarkBlue,
            darkCyan => ConsoleColor.DarkCyan,
            darkGray => ConsoleColor.DarkGray,
            darkGreen => ConsoleColor.DarkGreen,
            darkMagenta => ConsoleColor.DarkMagenta,
            darkRed => ConsoleColor.DarkRed,
            darkYellow => ConsoleColor.DarkYellow,
            gray => ConsoleColor.Gray,
            green => ConsoleColor.Green,
            mangenta => ConsoleColor.Magenta,
            red => ConsoleColor.Red,
            white => ConsoleColor.White,
            yellow => ConsoleColor.Yellow,
            _ => throw new FormHandlerException("You must select a valid console color.")
        };
    }
}
