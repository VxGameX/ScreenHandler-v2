using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ScreenHandler.Configurators;
using ScreenHandler.Exceptions;
using ScreenHandler.Models;
using ScreenHandler.Validators;

namespace ScreenHandler.Handlers;

public sealed class FormHandlerBuilder : IFormHandlerBuilder
{
    private readonly ILogger<FormHandlerBuilder> _log;
    private readonly IFormValidator _formValidator;
    private readonly ISectionConfigurator _sectionConfigurator;

    public Form Form { get; set; }
    public ICollection<Section> FormSections { get; set; }

    public FormHandlerBuilder(ILogger<FormHandlerBuilder> log, IFormValidator formValidator, ISectionConfigurator sectionConfigurator, string formPath)
    {
        _log = log;
        _formValidator = formValidator;
        _sectionConfigurator = sectionConfigurator;

        try
        {
            using (var file = new StreamReader(formPath))
            {
                var json = file.ReadToEnd()
                    .Trim();

                var newForm = JsonConvert.DeserializeObject<Form>(json)!;

                _formValidator.RegisterForm(newForm);
                Form = newForm;
                FormSections = Form.Sections!.ToList();
            }
        }
        catch (Exception ex)
        {
            _log.LogError("There was an error trying to get the Form from '{0}'.", formPath, ex);
            throw new FormHandlerBuilderException($"Could not find any screen file on path {formPath}");
        }
    }

    public IFormHandler Build() => new FormHandler(this);

    public ISectionConfigurator SectionsSettings() => _sectionConfigurator;
}
