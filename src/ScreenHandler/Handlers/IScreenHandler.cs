using ScreenHandler.Settings;

namespace ScreenHandler.Handlers;

public interface IScreenHandler
{
    ICollection<ConfigFile> screens { get; set; }
    ConfigFile entryPoint { get; set; }

    ScreenHandler NextScreen(string screenId);
    ScreenHandler Run();
    (bool, ConfigFile) ScreenValidation(string screenId);
    (bool, ConfigFile) ScreenValidation(IEnumerable<ConfigFile> screens);
}
