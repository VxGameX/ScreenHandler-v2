using Newtonsoft.Json;
using ScreenHandler.Exceptions;
using ScreenHandler.Models;
using ScreenHandler.Validators;

namespace ScreenHandler.Handlers;

public sealed class ScreenHandlerBuilder : IScreenHandlerBuilder
{
    private readonly IScreenValidator _screenValidator = new ScreenValidator();

    public ScreenDefinition Screen { get; set; }

    public ScreenHandlerBuilder(string screenPath)
    {
        try
        {
            using (var file = new StreamReader(screenPath))
            {
                var json = file.ReadToEnd()
                    .Trim();

                var newScreen = JsonConvert.DeserializeObject<ScreenDefinition>(json)!;

                _screenValidator.RegisterForm(newScreen);
                Screen = newScreen;
            }
        }
        catch
        {
            throw new FormHandlerBuilderException($"Could not find any screen file on path {screenPath}");
        }
    }

    public IScreenHandler Build() => new ScreenHandler(this);
}
