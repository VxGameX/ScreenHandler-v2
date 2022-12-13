using ScreenHandler.Settings;

namespace ScreenHandler.Validators;

internal class SectionValidator : ISectionValidator
{
    private const string _int = "int", _float = "float", _radioButton = "radiobutton",
       _text = "text", _checkBox = "checkbox";

    public void RunValidations(Form form)
    {
        if (form.Sections is null)
            throw new Exception("Config file 'sections' is empty. You must add at least 1 section to a form.");

        if (form.Sections.FirstOrDefault() is null)
            throw new Exception("Config file 'sections' is empty. You must add at least 1 section to a form.");

        SectionValidation(form.Sections);
    }

    private void SectionValidation(IEnumerable<Section> sections)
    {

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

            if (section.Input.Type is not _int and not _float and not _radioButton and not _text and not _checkBox)
                throw new Exception("Config file -> 'sections' -> 'input' -> 'type' is not a valid option. "
                + "You must specify 'text', 'int', 'checkbox' or 'radiobutton' input type.");

            if (section.Input.Type is _checkBox or _radioButton && section.Input.Options is null)
                throw new Exception($"Config file -> 'sections' -> 'input' -> 'options' is empty. "
                + $"You must specify options if type is {_checkBox} or {_radioButton}");

            if (section.Input.Type is _checkBox or _radioButton && !section.Input.Options!.Any())
                throw new Exception($"Config file -> 'sections' -> 'input' -> 'options' is empty. "
                + $"You must specify options if type is {_checkBox} or {_radioButton}");

            if (section.Input is not null)
                PreAnsweredValidation(section.Input);
        }
    }

    private void PreAnsweredValidation(Input input)
    {
        if (!string.IsNullOrWhiteSpace(input.Answer))
            throw new Exception($"Config file -> 'sections' -> 'input' -> 'options' is empty. "
                + $"You must specify options if type is {_checkBox} or {_radioButton}");

        if (input.SelectedOptions is not null)
            throw new Exception($"Config file -> 'sections' -> 'input' -> 'options' is empty. "
                    + $"You must specify options if type is {_checkBox} or {_radioButton}");
    }
}
