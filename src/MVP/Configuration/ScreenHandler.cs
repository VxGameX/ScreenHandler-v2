using MVP.Settings;

namespace MVP.Configuration;

public class ScreenHandler : IScreenHandler
{
    public IList<ConfigFile> screens { get; set; }
    public ConfigFile entryPoint { get; set; } = null!;
    private ConfigFile _currentScreen = null!;

    public ScreenHandler() => screens = new List<ConfigFile>();

    public ScreenHandler NextScreen()
    {
        var nextScreenIndex = screens.IndexOf(_currentScreen) + 1;
        var nextScreenId = screens[nextScreenIndex].Id;
        ShowScreen(nextScreenId);
        return this;
    }

    public ScreenHandler ShowScreen(string screenId)
    {
        ClearScreen();
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

    private static void ShowFields(ConfigFile file)
    {
        foreach (var field in file.Fields)
            do
            {
                ShowTitle(file.Title);
                Console.Write($"{(field.Required ? "*" : string.Empty)}{field.Name}\n>> ");
                var answer = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(answer) && field.Required)
                {
                    Console.Clear();
                    Console.WriteLine("Cannot leave required (*) fields empty.");
                    continue;
                }
                break;
            }
            while (true);
    }

    private static void CentralizeTitle(string title)
    {
        Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
        Console.WriteLine(title);
    }

    private static void SetTitle(Title title) => Console.Title = title.Display;

    private static void ShowTitle(Title title)
    {
        if (title.Centralized)
        {
            CentralizeTitle(title.Display);
            return;
        }
        ShowTitle(title.Display);
    }

    private static void ShowTitle(string title) => Console.WriteLine(title);

    private static void ClearScreen() => Console.Clear();
}
