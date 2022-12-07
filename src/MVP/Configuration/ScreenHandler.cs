using MVP.Settings;

namespace MVP.Configuration;
public class ScreenHandler
{
    public IList<ConfigFile> screens { get; set; }
    public ConfigFile entryPoint { get; set; } = null!;
    private ConfigFile _currentScreen = null!;

    public ScreenHandler() => screens = new List<ConfigFile>();

    private ScreenHandler SetTitle(string title)
    {
        Console.Title = title;
        return this;
    }

    public ScreenHandler NextScreen()
    {
        var nextScreenIndex = screens.IndexOf(_currentScreen) + 1;
        var nextScreenId = screens[nextScreenIndex].Id;
        ShowScreen(nextScreenId);
        return this;
    }

    public ScreenHandler ShowScreen(string screenId)
    {
        Console.Clear();
        var (isValid, file) = ScreenValidation(screenId);
        if (!isValid)
            throw new Exception($"Cannot show screen ({screenId}) because it is not registered.");

        SetTitle(file.Title);
        Console.WriteLine(file.Title);
        ShowFields(file);
        return this;
    }

    private static void ShowFields(ConfigFile file)
    {
        foreach (var field in file.Fields)
            do
            {
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
}
