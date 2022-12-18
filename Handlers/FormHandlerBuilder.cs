using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ScreenHandler.Exceptions;
using ScreenHandler.Models;
using ScreenHandler.Validators;

namespace ScreenHandler.Handlers;

public sealed class FormHandlerBuilder<TEntity> : IFormHandlerBuilder<TEntity>
{
    private readonly ILogger<FormHandlerBuilder<TEntity>> _log;
    private readonly IFormValidator _formValidator;
    private readonly IEnumerable<Section> _formSections;

    public Form Form { get; set; }

    public FormHandlerBuilder(ILogger<FormHandlerBuilder<TEntity>> log, IFormValidator formValidator, string formPath)
    {
        _log = log;
        _formValidator = formValidator;

        try
        {
            using (var file = new StreamReader(formPath))
            {
                var json = file.ReadToEnd()
                    .Trim();

                var newForm = JsonConvert.DeserializeObject<Form>(json)!;

                _formValidator.RegisterForm(newForm);
                Form = newForm;
                _formSections = Form.Sections.ToList();
                Form.Sections.Clear();
            }
        }
        catch (Exception ex)
        {
            _log.LogError("There was an error trying to get the Form from '{0}'.", formPath, ex);
            throw new FormHandlerBuilderException($"Could not find any screen file on path {formPath}");
        }
    }

    public IFormHandler<TEntity> Build() => new FormHandler<TEntity>(this);

    public IFormHandlerBuilder<TEntity> RegisterSection(string sectionId)
    {
        if (IsSectionRegistered(sectionId))
            throw new SectionConfigurationException($"Section '{sectionId}' is already registered.");

        try
        {
            var nextSection = _formSections.First(s => s.Id == sectionId);
            Form.Sections.Add(nextSection);
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
            var section = Form.Sections.First(rs => rs.Id == sectionId);
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
