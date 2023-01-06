using ConsoleScreenHandler.Models;
using ConsoleScreenHandler.Validators;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ConsoleScreenHandler.Handlers;

public sealed class ScreenHandlerBuilder : IScreenHandlerBuilder
{
    private readonly ILogger<ScreenHandlerBuilder> _logger;
    private readonly IValidator<Screen> _screenValidator;
    private readonly Func<IScreenHandler> _screenHandlerFactory;
    private readonly IActionHandlerFactory _actionHandlerFactory;

    private Screen _screen = null!;
    private Func<Section, string, bool> _answerValidation = null!;
    private Func<Section, string> _labelOutput = null!;
    private Func<Section, string, string> _notValidAnswerResponse = null!;

    public ScreenHandlerBuilder(ILogger<ScreenHandlerBuilder> logger, IValidator<Screen> screenValidator,
        Func<IScreenHandler> screenHandlerFactory, IActionHandlerFactory actionHandlerFactory)
    {
        _logger = logger;
        _screenValidator = screenValidator;
        _screenHandlerFactory = screenHandlerFactory;
        _actionHandlerFactory = actionHandlerFactory;
    }

    public IScreenHandler Build()
    {
        var newScreenHandler = _screenHandlerFactory();
        newScreenHandler.Screen = _screen;
        newScreenHandler.AnswerValidation = _answerValidation;
        newScreenHandler.LabelOutput = _labelOutput;
        newScreenHandler.NotValidAnswerResponse = _notValidAnswerResponse;
        newScreenHandler.ActionHandler = _actionHandlerFactory.Create(_screen.Actions);
        return newScreenHandler;
    }

    public IScreenHandlerBuilder ParseScreen(string screenPath)
    {
        using var file = new StreamReader(screenPath);
        var json = file.ReadToEnd();

        var newScreen = JsonConvert.DeserializeObject<Screen>(json)!;
        _screenValidator.Register(newScreen);

        _screen = newScreen;
        return this;
    }

    public IScreenHandlerBuilder SetAnswerValidation(Func<Section, string, bool> answerValidation)
    {
        _answerValidation = answerValidation;
        return this;
    }

    public IScreenHandlerBuilder SetLabelOutput(Func<Section, string> labelOutput)
    {
        _labelOutput = labelOutput;
        return this;
    }

    public IScreenHandlerBuilder SetNotValidAnswerResponse(Func<Section, string, string> notValidAnswerResponse)
    {
        _notValidAnswerResponse = notValidAnswerResponse;
        return this;
    }
}
