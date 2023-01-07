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

    private Func<Section, string, bool>? _answerValidation;
    private ConsoleColor _backgroundColor;
    private ConsoleColor _foregroundColor;
    private Func<Section, string>? _labelOutput;
    private Func<Section, string, string>? _notValidAnswerResponse;
    private Screen _screen = null!;
    private System.Action _screenPause = null!;
    private Func<string, string> _titleDisplay = null!;

    public ScreenHandlerBuilder(ILogger<ScreenHandlerBuilder> logger, IValidator<Screen> screenValidator,
        Func<IScreenHandler> screenHandlerFactory, IActionHandlerFactory actionHandlerFactory)
    {
        _logger = logger;
        _backgroundColor = Console.BackgroundColor;
        _foregroundColor = Console.ForegroundColor;

        _screenValidator = screenValidator;
        _screenHandlerFactory = screenHandlerFactory;
        _actionHandlerFactory = actionHandlerFactory;
    }

    public IScreenHandler Build()
    {
        var newScreenHandler = _screenHandlerFactory();

        newScreenHandler.Screen = _screen;
        newScreenHandler.BackgroundColor = _backgroundColor;
        newScreenHandler.ForegroundColor = _foregroundColor;

        if (_answerValidation is not null)
            newScreenHandler.AnswerValidation = _answerValidation;

        if (_labelOutput is not null)
            newScreenHandler.LabelOutput = _labelOutput;

        if (_notValidAnswerResponse is not null)
            newScreenHandler.NotValidAnswerResponse = _notValidAnswerResponse;

        if (_notValidAnswerResponse is not null)
            newScreenHandler.NotValidAnswerResponse = _notValidAnswerResponse;

        if (_screenPause is not null)
            newScreenHandler.ScreenPause = _screenPause;

        if (_titleDisplay is not null)
            newScreenHandler.TitleDisplay = _titleDisplay;

        return newScreenHandler;
    }

    public IScreenHandlerBuilder ParseScreen(string screenPath)
    {
        using var file = new StreamReader(screenPath);
        var json = file.ReadToEnd();

        var newScreen = JsonConvert.DeserializeObject<Screen>(json)!;
        _screenValidator.RunValidations(newScreen);
        _screenValidator.Register(newScreen);

        _screen = newScreen;
        return this;
    }

    public IScreenHandlerBuilder SetAnswerValidation(Func<Section, string, bool> answerValidation)
    {
        _answerValidation = answerValidation;
        return this;
    }

    public IScreenHandlerBuilder SetBackgroundColor(ConsoleColor backgroundColor)
    {
        _backgroundColor = backgroundColor;
        return this;
    }

    public IScreenHandlerBuilder SetForegroundColor(ConsoleColor foregroundColor)
    {
        _foregroundColor = foregroundColor;
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

    public IScreenHandlerBuilder SetScreenPause(System.Action screenPause)
    {
        _screenPause = screenPause;
        return this;
    }

    public IScreenHandlerBuilder SetTitleDisplay(Func<string, string> titleDisplay)
    {
        _titleDisplay = titleDisplay;
        return this;
    }
}
