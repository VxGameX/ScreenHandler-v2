using ConsoleScreenHandler.Exceptions;
using ConsoleScreenHandler.Helpers;
using ConsoleScreenHandler.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ConsoleScreenHandler.Handlers;

public sealed class ScreenHandler : IScreenHandler
{
    private readonly ILogger<ScreenHandler> _logger;
    private readonly IHandlerHelpers _handlerHelpers;
    public IActionHandler ActionHandler { get; set; } = null!;
    public ISectionHandler SectionHandler { get; set; } = null!;
    private readonly IResponse _screenResponse;
    private bool _isFormCompleted;

    public Screen Screen { get; set; } = null!;

    internal ScreenHandler(ILogger<ScreenHandler> logger, IHandlerHelpers handlerHelpers, IResponse screenResponse)
    {
        _logger = logger;
        _handlerHelpers = handlerHelpers;
        _screenResponse = screenResponse;
    }

    public void ShowScreen()
    {
        SetTitle();

        SectionHandler.ShowSections();
        ActionHandler.ShowActions();

        _isFormCompleted = true;
        Console.WriteLine("Exit code 0.");
    }

    public TEntity GetAnswer<TEntity>()
    {
        if (!_isFormCompleted)
            throw new ConsoleScreenHandlerException("Form is not yet completed");

        var response = JsonConvert.SerializeObject(_screenResponse.Data);

        var answer = JsonConvert.DeserializeObject<TEntity>(response);

        if (answer is null)
            throw new ConsoleScreenHandlerException("Answer is not available.");

        return answer;
    }

    private void SetTitle()
    {
        Console.Title = Screen.Title;
        _handlerHelpers.ScreenTitle = Screen.Title;
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
