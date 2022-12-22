using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ConsoleScreenHandler.Exceptions;
using ConsoleScreenHandler.Models;
using ConsoleScreenHandler.Validators;

namespace ConsoleScreenHandler.Handlers;

public sealed class ScreenHandlerBuilder : IHandlerBuilder<ScreenHandler>
{
    private readonly IScreenValidator _screenValidator;
    private readonly ILogger<ScreenHandlerBuilder> _logger;

    public ScreenHandlerBuilder(ILogger<ScreenHandlerBuilder> logger, IScreenValidator screenValidator)
    {
        _logger = logger;
        _screenValidator = screenValidator;
    }

    public ScreenHandler Build(string screenPath)
    {
        try
        {
            using (var file = new StreamReader(screenPath))
            {
                var json = file.ReadToEnd()
                    .Trim();

                var newScreen = JsonConvert.DeserializeObject<ScreenDefinition>(json)!;

                _screenValidator.RegisterForm(newScreen);

                var logger = CreateHandlerLogger();

                return new(logger, newScreen);
            }
        }
        catch
        {
            throw new ScreenHandlerBuilderException($"Could not find any screen file on path {screenPath}");
        }
    }

    private ILogger<ScreenHandler> CreateHandlerLogger()
    {
        using var loggerFactory = LoggerFactory.Create(loggingBuilder =>
        {
            loggingBuilder.SetMinimumLevel(LogLevel.Trace)
                    .AddConsole();
        });

        var logger = loggerFactory.CreateLogger<ScreenHandler>();
        return logger;
    }
}
