namespace ConsoleScreenHandler.Handlers;

public interface IActionHandlerFactory
{
    IActionHandler Create(IEnumerable<Models.Action> actions);
}
