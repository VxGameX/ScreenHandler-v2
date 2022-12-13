using Newtonsoft.Json;
using ScreenHandler.Configurators;
using ScreenHandler.Exceptions;
using ScreenHandler.Settings;
using ScreenHandler.Validators;

namespace ScreenHandler.Handlers;

public sealed class FormHandlerBuilder : IFormHandlerBuilder
{
    private IFormValidator _formValidator;
    internal Form Form { get; set; }
    internal ICollection<Section> FormSections { get; set; }

    public FormHandlerBuilder(string formPath)
    {
        FormSections = new List<Section>();

        using (var file = new StreamReader(formPath))
        {
            _formValidator = new FormValidator();

            var json = file.ReadToEnd()
                .Trim();

            var newForm = JsonConvert.DeserializeObject<Form>(json);
            if (newForm is null)
                throw new FormHandlerBuilderException($"Could not find any screen file on path {formPath}");

            _formValidator.RegisterForm(newForm);

            Form = newForm;

            FormSections = Form.Sections!.ToList();
        }
    }

    public IFormHandler Build() => new FormHandler(this);

    public ISectionConfigurator SectionsSettings() => new SectionConfigurator(this);
}
