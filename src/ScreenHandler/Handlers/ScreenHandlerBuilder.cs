using ScreenHandler.Settings;
using Newtonsoft.Json;

namespace ScreenHandler.Handlers;

public class ScreenHandlerBuilder : IScreenHandlerBuilder
{
    private ScreenHandler _screenHandler;

    public ScreenHandlerBuilder() => _screenHandler = new();

    public ScreenHandler Build() => _screenHandler;

    public ScreenHandlerBuilder RegisterSingleScreen(string path)
    {
        using (var file = new StreamReader(path))
        {
            var json = file.ReadToEnd().Trim();
            var newScreen = JsonConvert.DeserializeObject<ConfigFile>(json);
            if (newScreen is null)
                throw new Exception($"Could not find any screen file on path {path}");

            var (isRegistered, screen) = _screenHandler.ScreenValidation(newScreen.Id);
            if (isRegistered)
                throw new Exception($"Screen ID {newScreen.Id} is already registered.");

            _screenHandler.screens.Add(newScreen);
            return this;
        }
    }

    public ScreenHandlerBuilder RegisterMultipleScreens(string path)
    {
        using (var file = new StreamReader(path))
        {
            var json = file.ReadToEnd().Trim();
            var newScreens = JsonConvert.DeserializeObject<IEnumerable<ConfigFile>>(json);
            if (newScreens is null)
                throw new Exception($"Could not find any screen file on path {path})");

            var (isRegistered, existingScreen) = _screenHandler.ScreenValidation(newScreens);
            if (isRegistered)
                throw new Exception($"Screen ID {existingScreen.Id} is already registered.");

            foreach (var screen in newScreens)
                _screenHandler.screens.Add(screen);

            return this;
        }
    }

    public ScreenHandlerBuilder SetEntryPoint(string screenId)
    {
        var (isValid, entryPoint) = _screenHandler.ScreenValidation(screenId);
        if (!isValid)
            throw new Exception($"Cannot set entry point. Screen ({screenId}) not registered.");

        _screenHandler.entryPoint = entryPoint;
        return this;
    }
}
