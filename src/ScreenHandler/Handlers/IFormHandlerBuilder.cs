using ScreenHandler.Configurators;
using ScreenHandler.Settings;

namespace ScreenHandler.Handlers;

public interface IFormHandlerBuilder
{
    IFormHandler Build();
    ISectionConfigurator SetSectionsOrder();
}
