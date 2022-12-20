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
        } while (true);
    }

    private void RunAction(char button)
    {
        try
        {
            var selectedAction = _actions.First(a => a.Button == button);

            var type = Type.GetType(selectedAction.Handler);

            if (type is null)
                throw new Exception("Could not find handler.");

            var handler = Activator.CreateInstance(type);
            var handlerAction = type.GetMethod(selectedAction.HandlerAction);

            if (handlerAction is null)
                throw new Exception("Could not find handler action.");

            handlerAction.Invoke(handler, null);
        }
        catch
        {
            Console.WriteLine("You must select a valid action.");
        }
    }

    private void SetCurrentAction(Models.Action action) => _currentAction = action;

    private void ShowActionName() => Console.WriteLine($"{_currentAction.Name} ({_currentAction.Button}){Environment.NewLine}");

    private bool IsValidOption(char option) => !char.IsWhiteSpace(option);
}
