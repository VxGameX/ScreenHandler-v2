using ScreenHandler.Settings;

namespace ScreenHandler.Validators;

internal class SectionValidator : ISectionValidator
{
    public void RunValidations(ConfigFile form)
    {
        if (form.Sections is null)
            throw new Exception("Config file 'sections' is empty. You must add at least 1 section to a form.");

        if (form.Sections.FirstOrDefault() is null)
            throw new Exception("Config file 'sections' is empty. You must add at least 1 section to a form.");

        SectionValidation(form.Sections);
    }

    private void SectionValidation(IEnumerable<Section> sections)
    {
        const string @int = "int", @float = "float", radioButton = "radiobutton",
           text = "text", checkBox = "checkbox";

        foreach (var section in sections)
        {
            if (string.IsNullOrWhiteSpace(section.Id))
                throw new Exception("Config file -> 'sections' -> 'Id' is empty. "
                + "You must specify an ID to a section.");

            if (string.IsNullOrWhiteSpace(section.Label))
                throw new Exception("Config file -> 'sections' -> 'Label' is empty. "
                + "You must add at least 1 section to a form.");

            if (section.Input is null)
                throw new Exception("Config file -> 'sections' -> 'Input' is empty. "
                + "You must add at least 1 section to a form.");

            if (string.IsNullOrWhiteSpace(section.Input.Type))
                throw new Exception("Config file -> 'sections' -> 'Input' -> 'Type' is empty. "
                + "You must add at least 1 section to a form.");

            if (section.Input.Type is not @int and not @float and not radioButton and not text and not checkBox)
                throw new Exception("Config file -> 'sections' -> 'input' -> 'type' is not a valid option. "
                + "You must specify 'text', 'int', 'checkbox' or 'radiobutton' input type.");

            if (section.Input.Type is checkBox or radioButton && section.Input.Options is null)
                throw new Exception($"Config file -> 'sections' -> 'input' -> 'options' is empty. "
                + "You must specify options if type is {checkBox} or {radioButton}");

            if (section.Input.Type is checkBox or radioButton && section.Input.Options?.FirstOrDefault() is null)
                throw new Exception($"Config file -> 'sections' -> 'input' -> 'options' is empty. "
                + "You must specify options if type is {checkBox} or {radioButton}");
        }
    }

    private void PreAnsweredValidation()
    {
        
    }
}
