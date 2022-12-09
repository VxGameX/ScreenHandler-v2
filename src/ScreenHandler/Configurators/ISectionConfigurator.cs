using ScreenHandler.Settings;

namespace ScreenHandler.Configurators;

public interface ISectionConfigurator
{
    ISectionConfigurator SetEntryPoint(Section entryPoint);
    ISectionConfigurator SetNextSection(Section nextSection);
    ISectionConfigurator SetSectionsOrder(params string[] sectionsId);
}
