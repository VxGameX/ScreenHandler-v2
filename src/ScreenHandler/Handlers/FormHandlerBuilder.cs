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

    public IFormHandler Build()
    {
        FormStructValidation();
        return new FormHandler(this);
    }

    public ISectionConfigurator SectionsSettings() => new SectionConfigurator(this);

    private bool IsFormEmpty(ConfigFile form)
    {
        if (form.Sections is null)
            return true;

        if (form.Sections.FirstOrDefault() is null)
            return true;

        return false;
    }

    private bool IsFormRegistered(string formId)
    {
        // var form = _registeredForms.FirstOrDefault(c => c.Id == formId);
        // if (form is null)
        //     return false;
        return false;
    }

    private bool IsFormRegistered(ConfigFile form)
    {
        // if (!_registeredForms.Contains(form))
        //     return false;
        return true;
    }

    private bool IsFormValid(ConfigFile form)
    {
        if (IsFormEmpty(form))
            throw new Exception("Form does not contains sections");

        return true;
    }

    private void FormStructValidation()
    {
        if (Form.Type != "form")
            throw new Exception($"Config file 'type' is {(string.IsNullOrWhiteSpace(Form.Type) ? "empty" : $"'{Form.Type}'")}. You must specify type 'form'.");

        if (string.IsNullOrWhiteSpace(Form.Id))
            throw new Exception("Config file 'id' is empty. You must specify a form id.");

        if (Form.Title is null)
            throw new Exception("Config file 'title' is empty. You must assing a title to a form.");

        if (string.IsNullOrWhiteSpace(Form.Title.Label))
            throw new Exception("Title 'label' is empty. You must assign a label to the form's title.");

        // if ()

        if (Form.Sections is null)
            throw new Exception("Config file 'sections' is empty. You must add at least 1 section to a form.");
    }
}
