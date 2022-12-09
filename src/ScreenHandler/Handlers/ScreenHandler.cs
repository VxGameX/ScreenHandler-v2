using ScreenHandler.Settings;

namespace ScreenHandler.Handlers;

public partial class ScreenHandler : IScreenHandler
{
    public ICollection<ConfigFile> screens { get; set; }
    public ConfigFile entryPoint { get; set; } = null!;
    private ConfigFile _currentScreen = null!;

    public ScreenHandler() => screens = new List<ConfigFile>();

    public static IScreenHandlerBuilder CreateBuilder() => new ScreenHandlerBuilder();

    public ScreenHandler Run()
    {
        _currentScreen = entryPoint;
        NextScreen(_currentScreen.Id);

        return this;
    }

    // public ScreenHandler NextScreen()
    // {
    //     var nextScreenIndex = screens.IndexOf(_currentScreen) + 1;
    //     var nextScreenId = screens[nextScreenIndex].Id;
    //     NextScreen(nextScreenId);
    //     return this;
    // }

    public ScreenHandler NextScreen(string screenId)
    {
        var (isValid, screen) = ScreenValidation(screenId);
        if (!isValid)
            throw new Exception($"Cannot show screen ({screenId}) because it is not registered.");

        SetCurrentScreen(screen);
        SetTitle(screen.Title);

        ShowFields(screen);
        return this;
    }

    public (bool, ConfigFile) ScreenValidation(string screenId)
    {
        var file = screens.FirstOrDefault(c => c.Id == screenId);
        if (file is null)
            return (false, null!);
        return (true, file);
    }

    public (bool, ConfigFile) ScreenValidation(IEnumerable<ConfigFile> screens)
    {
        foreach (var screen in screens)
            if (this.screens.Contains(screen))
                return (true, screen);

        return (false, null!);
    }

    private void SetCurrentScreen(ConfigFile screen) => _currentScreen = screen;

    private static void ShowFields(ConfigFile screen)
    {
        foreach (var section in screen.Sections!)
            do
            {
                ClearScreen(screen.Title);
                Console.Write($"{(section.Required ? "*" : string.Empty)}{section.Label}\n>> ");
                var answer = Console.ReadLine();

                if (IsValidAnswer(section, answer))
                    break;

                ClearScreen(screen.Title);
                Console.Write("Cannot leave required (*) fields empty.");
                Pause();
            }
            while (true);
    }

    private static void Pause() => Console.ReadKey();

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

    private static void ClearScreen(Title title)
    {
        Console.Clear();
        ShowTitle(title);
    }
}
