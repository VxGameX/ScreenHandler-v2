using ScreenHandler.Models;

namespace ScreenHandler.Handlers;

public sealed class ScreenHandler : IHandler
{
    private readonly ScreenDefinition _screen;
    private readonly IHandler _sectionHandler;
    private readonly IHandler _actionHandler;
    private bool _isFormCompleted;

    public string Title => _screen.Title;
    public IEnumerable<Section> Sections => _screen.Sections;
    public IEnumerable<Models.Action> Actions => _screen.Actions;

    public ScreenHandler(ScreenHandlerBuilder builder)
    {
        _screen = builder.Screen;
        _sectionHandler = new SectionHandler(this);
        _actionHandler = new ActionHandler(this);
    }

    public static IHandlerBuilder<ScreenHandler> CreateBuilder(string formPath) => new ScreenHandlerBuilder(formPath);

    public void Run()
    {
        SetTitle();

        _sectionHandler.Run();
        _actionHandler.Run();

        _isFormCompleted = true;
        Console.WriteLine("Exit code 0.");
    }

    // public TEntity GetAnswer<TEntity>()
    // {
    //     if (!_isFormCompleted)
    //         throw new FormHandlerException("Form is not yet completed");

    //     var x = JsonConvert.SerializeObject("");

    //     var answer = JsonConvert.DeserializeObject<TEntity>(x);

    //     if (answer is null)
    //         throw new FormHandlerException("Answer is not available.");

    //     return answer;
    // }

    private void SetTitle() => Console.Title = _screen.Title;

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
