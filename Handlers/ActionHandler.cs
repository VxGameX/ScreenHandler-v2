using ConsoleScreenHandler.Helpers;
using ConsoleScreenHandler.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleScreenHandler.Handlers;

public class ActionHandler : IActionHandler
{
    private readonly ILogger<ActionHandler> _logger;
    private readonly IHandlerHelpers _handlerHelper;
    private readonly IOptions<ConsoleScreenHandlerOptions> _options;
    private Models.Action _currentAction = null!;

    public IEnumerable<Models.Action> Actions { get; set; } = null!;

    public ActionHandler(ILogger<ActionHandler> logger, IHandlerHelpers handlerHelper, IOptions<ConsoleScreenHandlerOptions> options)
    {
        _logger = logger;
        _handlerHelper = handlerHelper;
        _options = options;
    }

    public void ShowActions()
    {
        do
        {
            _handlerHelper.ClearScreen();
            foreach (var action in Actions)
            {
                SetCurrentAction(action);
                ShowActionName();
            }

            Console.Write(">> ");
            var selectedAction = Console.ReadLine() ?? string.Empty;

            if (!IsValidOption(selectedAction))
            {
                _handlerHelper.ClearScreen();
                Console.Write("You must select an option.");
                _handlerHelper.Pause();
                continue;
            }
            RunAction(selectedAction);
            break;
        } while (true);
    }

    private void RunAction(string button)
    {
        var options = _options.Value;
        var selectedAction = Actions.FirstOrDefault(a => a.Button == button);

        if (selectedAction is null)
        {
            _handlerHelper.ClearScreen();
            Console.Write("You must select a valid action.");
            _handlerHelper.Pause();
            return;
        }

        var type = options.ExecutingAssembly.GetType(selectedAction.Handler);

        if (type is null)
            throw new Exception($"Could not find handler '{selectedAction.Handler}'.");

        var handler = Activator.CreateInstance(type);
        var handlerAction = type.GetMethod(selectedAction.HandlerAction);

        if (handlerAction is null)
            throw new Exception($"Could not find handler action'{selectedAction.HandlerAction}'.");

        handlerAction.Invoke(handler, null);
    }

    private void SetCurrentAction(Models.Action action) => _currentAction = action;

    private void ShowActionName() => Console.WriteLine($"{_currentAction.Name} ({_currentAction.Button}){Environment.NewLine}");

    private bool IsValidOption(string option) => !string.IsNullOrWhiteSpace(option);
}
