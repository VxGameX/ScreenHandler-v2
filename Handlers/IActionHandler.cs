namespace ConsoleScreenHandler.Handlers
{
    public interface IActionHandler
    {
        IEnumerable<Models.Action> Actions { get; set; }

        void ShowActions();
    }
}