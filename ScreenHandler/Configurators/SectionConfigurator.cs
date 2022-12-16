using ScreenHandler.Exceptions;
using ScreenHandler.Handlers;
using ScreenHandler.Settings;

namespace ScreenHandler.Configurators;

public class SectionConfigurator : ISectionConfigurator
{
    private FormHandlerBuilder _formHandlerBuilder;
    private Form _form;
    private Section _entryPoint = null!;
    private static ICollection<Section> _sectionsOrder = null!;

    public SectionConfigurator(FormHandlerBuilder formHandlerBuilder)
    {
        _form = formHandlerBuilder.Form;
        _sectionsOrder = new List<Section>();
        _formHandlerBuilder = formHandlerBuilder;
    }

    public void SaveOrderSettings() => _formHandlerBuilder.FormSections = _sectionsOrder;

    public ISectionConfigurator SetEntryPoint(string sectionId)
    {
        if (_entryPoint is not null)
            throw new SectionConfigurationException($"Cannot override entry point. Current entry point {_entryPoint.Id}");

        var entryPoint = GetSection(sectionId);
        _entryPoint = entryPoint;
        _sectionsOrder.Add(_entryPoint);
        return this;
    }

    public ISectionConfigurator SetNextSection(string sectionsId)
    {
        if (_entryPoint is null)
            throw new SectionConfigurationException("You must set an entry point before setting a next section.");

        var nextSection = GetSection(sectionsId);
        _sectionsOrder.Add(nextSection);
        return this;
    }

    private Section GetSection(string sectionId)
    {
        if (!IsSectionRegistered(sectionId))
            throw new SectionConfigurationException($"Section {sectionId} is not registered.");

        var section = _form.Sections!.First(rf => rf.Id == sectionId);
        return section;
    }

    private bool IsSectionRegistered(string sectionId)
    {
        var section = _form.Sections!.FirstOrDefault(rs => rs.Id == sectionId);
        if (section is null)
            return false;

        return true;
    }
}