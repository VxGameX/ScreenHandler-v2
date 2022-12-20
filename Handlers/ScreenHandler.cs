using Newtonsoft.Json;
using ScreenHandler.Exceptions;
using ScreenHandler.Models;

namespace ScreenHandler.Handlers;

public sealed class ScreenHandler : IScreenHandler
{
    private const string _radioButton = "radiobutton";
    private const string _checkBox = "checkbox";

    private Section _currentSection = null!;
    private bool _isFormCompleted;

    public ScreenDefinition Screen { get; set; }

    public ScreenHandler(IScreenHandlerBuilder builder) => Screen = builder.Screen;

    public static IScreenHandlerBuilder CreateBuilder(string formPath) => new ScreenHandlerBuilder(formPath);

    public void Run()
    {
        ClearScreen();
        SetTitle();
        foreach (var section in Screen.Sections)
        {
            SetCurrentSection(section);
            ShowSection();
        }

        _isFormCompleted = true;
        Console.WriteLine("Exit code 0.");
    }

    public TEntity GetAnswer<TEntity>()
    {
        if (!_isFormCompleted)
            throw new FormHandlerException("Form is not yet completed");

        var x = JsonConvert.SerializeObject("");

        var answer = JsonConvert.DeserializeObject<TEntity>(x);

        if (answer is null)
            throw new FormHandlerException("Answer is not available.");

        return answer;
    }

    private void SetCurrentSection(Section section) => _currentSection = section;

    private void ShowSection()
    {
        do
        {
            ClearScreen();
            ShowLabel();

            Console.Write(">> ");
            var answer = Console.ReadLine() ?? string.Empty;

            if (IsValidAnswer(answer))
                break;

            ClearScreen();
            Console.Write("Cannot leave required (*) sections empty.");
            Pause();
        }
        while (true);
    }

    private void ShowLabel() => Console.WriteLine($"{_currentSection.Label} {(_currentSection.Required ? "*" : string.Empty)}{Environment.NewLine}");

    private static void Pause() => Console.ReadKey(true);

    private bool IsValidAnswer(string answer) => !(_currentSection.Required && string.IsNullOrWhiteSpace(answer));

    private void SetTitle() => Console.Title = Screen.Title;

    private void ShowTitle()
    {
        Console.WriteLine($"{Screen.Title}{Environment.NewLine}");
    }

    private void ClearScreen()
    {
        Console.ResetColor();
        Console.Clear();
        ShowTitle();
    }

    // private void CentralizeTitle()
    // {
    //     Console.SetCursorPosition((Console.WindowWidth - Screen.Title.Length) / 2, Console.CursorTop);
    //     Console.WriteLine($"{Screen.Title}{Environment.NewLine}");
    // }

    // private void CentralizeTitle()
    // {
    //     if (Screen.Title.Centralized)
    //     {
    //         SetTitleColors();
    //         CentralizeTitle();
    //         if (Screen.Body is not null)
    //             SetBodyColors();
    //         return;
    //     }
    // }

    // private void SetBodyColors()
    // {
    //     if (Screen.Body.ForegroundColor is not null)
    //         Console.ForegroundColor = GetColor(Screen.Body.ForegroundColor);

    //     if (Screen.Body.BackgroundColor is not null)
    //         Console.BackgroundColor = GetColor(Screen.Body.BackgroundColor);
    // }

    // private void SetTitleColors()
    // {
    //     if (Screen.Title.ForegroundColor is not null)
    //         Console.ForegroundColor = GetColor(Screen.Title.ForegroundColor);

    //     if (Screen.Title.BackgroundColor is not null)
    //         Console.BackgroundColor = GetColor(Screen.Title.BackgroundColor);
    // }

    // private ConsoleColor GetColor(string color)
    // {
    //     const string black = "black", blue = "blue", cyan = "cyan",
    //         darkBlue = "darkBlue", darkCyan = "darkCyan", darkGray = "darkGray",
    //         darkGreen = "darkGreen", darkMagenta = "darkMagenta", darkRed = "darkRed",
    //         darkYellow = "darkYello", gray = "gray", green = "green",
    //         mangenta = "magenta", red = "red", white = "white", yellow = "yellow";

    //     return color switch
    //     {
    //         black => ConsoleColor.Black,
    //         blue => ConsoleColor.Blue,
    //         cyan => ConsoleColor.Cyan,
    //         darkBlue => ConsoleColor.DarkBlue,
    //         darkCyan => ConsoleColor.DarkCyan,
    //         darkGray => ConsoleColor.DarkGray,
    //         darkGreen => ConsoleColor.DarkGreen,
    //         darkMagenta => ConsoleColor.DarkMagenta,
    //         darkRed => ConsoleColor.DarkRed,
    //         darkYellow => ConsoleColor.DarkYellow,
    //         gray => ConsoleColor.Gray,
    //         green => ConsoleColor.Green,
    //         mangenta => ConsoleColor.Magenta,
    //         red => ConsoleColor.Red,
    //         white => ConsoleColor.White,
    //         yellow => ConsoleColor.Yellow,
    //         _ => throw new FormHandlerException("You must select a valid console color.")
    //     };
    // }
}
