using Microsoft.Extensions.Logging;
using ScreenHandler.Exceptions;
using ScreenHandler.Handlers;
using ScreenHandler.Models;

namespace ScreenHandler.Configurators;

public sealed class SectionConfigurator : ISectionConfigurator
{
    private readonly ILogger<SectionConfigurator> _log;
    private readonly IEnumerable<Section> _fileSections;
    private IFormHandlerBuilder _formHandlerBuilder;

    public SectionConfigurator(ILogger<SectionConfigurator> log, IFormHandlerBuilder formHandlerBuilder)
    {
        _log = log;
        _formHandlerBuilder = formHandlerBuilder;

        try
        {
            _fileSections = _formHandlerBuilder.FormSections;
        }
        catch (Exception ex)
        {
            _log.LogError("There was an error getting the sections.", ex);
            throw new FormHandlerBuilderException($"There was an error getting the sections. {ex.Message}");
        }
    }

    public ISectionConfigurator RegisterSection(string sectionId)
    {
        if (IsSectionRegistered(sectionId))
            throw new SectionConfigurationException($"Section '{sectionId}' is already registered.");

        try
        {
            var nextSection = _fileSections.First(s => s.Id == sectionId);
            _formHandlerBuilder.FormSections.Add(nextSection);
            _log.LogDebug("Section {0} registered.", sectionId);
            return this;
        }
        catch (Exception ex)
        {
            _log.LogError("There was an error registering the section.", ex);
            throw;
        }
    }

    private bool IsSectionRegistered(string sectionId)
    {
        try
        {
            var section = _formHandlerBuilder.FormSections.First(rs => rs.Id == sectionId);
            _log.LogDebug("Section '{0}' is already registered.", section.Id);
            return true;
        }
        catch
        {
            _log.LogDebug("Section '{0}' is not registered.", sectionId);
            return false;
        }
    }
}
