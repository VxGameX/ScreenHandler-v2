using ScreenHandler.Settings;

namespace ScreenHandler.Validators;

public interface ISectionValidator
{
    void RunValidations(ConfigFile form);
}
