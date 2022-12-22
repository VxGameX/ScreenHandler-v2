using ScreenHandler.Configurators;
using ScreenHandler.Helpers;

namespace ScreenHandler.Handlers;

public class ActionHandler : IHandler
{
    private readonly IEnumerable<Models.Action> _actions;
    private Models.Action _currentAction = null!;

    public ActionHandler(ScreenHandler handler) => _actions = handler.Actions;

    public void Run()
    {
        do
        {
            HandlerHelpers.ClearScreen();
            foreach (var action in _actions)
            {
                SetCurrentAction(action);
                ShowActionName();
            }

            Console.Write(">> ");
            var selectedAction = Console.ReadKey(true).KeyChar;

            if (!IsValidOption(selectedAction))
            {
                HandlerHelpers.ClearScreen();
                Console.Write("You must select an option.");
                HandlerHelpers.Pause();
                continue;
            }
            RunAction(selectedAction);
            break;
        } while (true);
    }

    private void RunAction(char button)
    {
        var selectedAction = _actions.FirstOrDefault(a => a.Button == button);

        if (selectedAction is null)
        {
            HandlerHelpers.ClearScreen();
            Console.Write("You must select a valid action.");
            HandlerHelpers.Pause();
            return;
        }

        var type = AssemblyConfigurator.ExecutingAssembly.GetType(selectedAction.Handler);

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

    private bool IsValidOption(char option) => !char.IsWhiteSpace(option);
}
