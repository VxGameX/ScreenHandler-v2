namespace ScreenHandler.Handlers;

public interface IFormHandler
{
    IFormHandler ClearAnswers();
    IFormHandler NextSection();
    IFormHandler PreviousSection();
    IFormHandler ReviewAnswers();
    IFormHandler RestartForm();
    void Run();
}
