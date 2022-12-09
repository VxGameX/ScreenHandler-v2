using Newtonsoft.Json;
using ScreenHandler.Configurators;
using ScreenHandler.Settings;

namespace ScreenHandler.Handlers;

public sealed class FormHandlerBuilder : IFormHandlerBuilder
{
    public ConfigFile Form { get; set; }
    public ICollection<Section> FormSections { get; set; }

    public FormHandlerBuilder(string formPath)
    {
        FormSections = new List<Section>();

        using (var file = new StreamReader(formPath))
        {
            var json = file.ReadToEnd()
                .Trim();
            var newForm = JsonConvert.DeserializeObject<ConfigFile>(json);
            if (newForm is null)
                throw new Exception($"Could not find any screen file on path {formPath}");

            var isRegistered = IsFormRegistered(newForm.Id);
            if (isRegistered)
                throw new Exception($"Screen ID {newForm.Id} is already registered.");

            Form = newForm;
            // TODO: Set sections validation bedore assign
            FormSections = Form.Sections!.ToList();
        }
    }

    public IFormHandler Build() => new FormHandler(Form, FormSections);

    public ISectionConfigurator SetSectionsOrder() => new SectionConfigurator(this);

    private bool IsFormEmpty(ConfigFile form)
    {
        if (form.Sections is null)
            return true;

        if (form.Sections.FirstOrDefault() is null)
            return true;

        return false;
    }

    public bool IsFormRegistered(string formId)
    {
        // var form = _registeredForms.FirstOrDefault(c => c.Id == formId);
        // if (form is null)
        //     return false;
        return false;
    }

    public bool IsFormRegistered(ConfigFile form)
    {
        // if (!_registeredForms.Contains(form))
        //     return false;
        return true;
    }

    public bool IsFormValid(ConfigFile form)
    {
        if (IsFormEmpty(form))
            throw new Exception("Form does not contains sections");

        return true;
    }
}
