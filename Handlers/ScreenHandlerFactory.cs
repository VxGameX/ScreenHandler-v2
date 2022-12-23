using ConsoleScreenHandler.Exceptions;
using ConsoleScreenHandler.Models;
using ConsoleScreenHandler.Validators;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ConsoleScreenHandler.Handlers;

public sealed class ScreenHandlerFactory : IScreenHandlerFactory
{
    private readonly ILogger<ScreenHandlerFactory> _logger;
    private readonly IValidator<Screen> _screenValidator;
    private readonly Func<IScreenHandler> _screenHandlerFactory;
    private readonly IActionHandlerFactory _actionHandlerFactory;
    private readonly ISectionHandlerFactory _sectionHandlerFactory;

    public ScreenHandlerFactory(ILogger<ScreenHandlerFactory> logger, IValidator<Screen> screenValidator,
        Func<IScreenHandler> screenHandlerFactory, IActionHandlerFactory actionHandlerFactory, ISectionHandlerFactory sectionHandlerFactory)
    {
        _logger = logger;
        _screenValidator = screenValidator;
        _screenHandlerFactory = screenHandlerFactory;
        _actionHandlerFactory = actionHandlerFactory;
        _sectionHandlerFactory = sectionHandlerFactory;
    }

    public IScreenHandler Create(string screenPath)
    {
        try
        {
            var newScreenHandler = _screenHandlerFactory();
            using var file = new StreamReader(screenPath);
            var json = file.ReadToEnd()
                .Trim();

            var newScreen = JsonConvert.DeserializeObject<Screen>(json)!;

            _screenValidator.Register(newScreen);
            newScreenHandler.Screen = newScreen;
            newScreenHandler.ActionHandler = _actionHandlerFactory.Create(newScreen.Actions);
            newScreenHandler.SectionHandler = _sectionHandlerFactory.Create(newScreen.Sections);
            return newScreenHandler;
        }
        catch
        {
            throw new ScreenHandlerBuilderException($"Could not find any screen file on path {screenPath}");
        }
    }
}
