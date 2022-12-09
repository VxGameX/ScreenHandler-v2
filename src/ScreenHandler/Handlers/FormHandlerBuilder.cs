using Newtonsoft.Json;
using ScreenHandler.Configurators;
using ScreenHandler.Settings;

namespace ScreenHandler.Handlers;

public sealed class FormHandlerBuilder : IFormHandlerBuilder
{
    private static IList<ConfigFile> _registeredForms = new List<ConfigFile>();
    private IList<Section> _formSections;

    public FormHandlerBuilder(string formPath)
    {
        _formSections = new List<Section>();

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
        }
    }

    public IFormHandler Build() => new FormHandler(this);

    public ISectionConfigurator SetSectionsOrder() => new SectionConfigurator();

    private bool IsFormRegistered(string formId)
    {
        var form = _registeredForms.FirstOrDefault(c => c.Id == formId);
        if (form is null)
            return false;
        return true;
    }
}
