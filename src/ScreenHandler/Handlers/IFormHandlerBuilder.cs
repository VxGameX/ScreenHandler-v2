using ScreenHandler.Configurators;

namespace ScreenHandler.Handlers;

public interface IFormHandlerBuilder
{
    IFormHandler Build();
    public ISectionConfigurator SetSectionsOrder();
}
