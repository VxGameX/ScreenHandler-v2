namespace ScreenHandler.Configurators;

public interface ISectionConfigurator
{
    ISectionConfigurator SetEntryPoint(string sectionsId);
    ISectionConfigurator SetNextSection(string sectionsId);
    void SaveOrderSettings();
}
