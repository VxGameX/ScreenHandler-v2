using ScreenHandler.Settings;

namespace ScreenHandler.Handlers;

public interface IFormHandler
{
    IFormHandler ClearAnswers();
    IFormHandler NextSection();
    IFormHandler PreviousSection();
    IFormHandler Save();
    void Start();
}
