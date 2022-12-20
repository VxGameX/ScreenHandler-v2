using ScreenHandler.Models;

namespace ScreenHandler.Validators;

public interface IScreenValidator
{
    void RegisterForm(ScreenDefinition screen);
}
