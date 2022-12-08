namespace ScreenHandler.Handlers;

public interface IFormHandler
{
    IFormHandler NextSection();
    IFormHandler PreviousSection();
}
