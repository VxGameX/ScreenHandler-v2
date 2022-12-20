using ScreenHandler.Exceptions;
using ScreenHandler.Models;

namespace ScreenHandler.Validators;

public class ScreenValidator : IScreenValidator
{
    private static ICollection<ScreenDefinition> _registeredForms = new List<ScreenDefinition>();

    public void RegisterForm(ScreenDefinition form)
    {
        if (IsFormRegistered(form.Id))
            throw new ScreenStructException($"Form ID {form.Id} is already registered.");

        RunValidations(form);
        _registeredForms.Add(form);
    }

    private void RunValidations(ScreenDefinition form)
    {
        if (string.IsNullOrWhiteSpace(form.Id))
            throw new ScreenStructException("Config file 'id' is empty. You must specify a form id.");

        if (string.IsNullOrWhiteSpace(form.Title))
            throw new ScreenStructException("Config file 'title' is empty. You must assing a title to a form");

        if (form.Sections is null || !form.Sections.Any())
            throw new SectionValidationException("Config file 'sections' is empty. You must add at least 1 section to a form.");

        // Section validations
        foreach (var section in form.Sections)
        {
            if (string.IsNullOrWhiteSpace(section.Label))
                throw new SectionValidationException("Config file -> 'sections' -> 'label' is empty. You must add at least 1 section to a form.");

            if (string.IsNullOrWhiteSpace(section.Input))
                throw new SectionValidationException("Config file -> 'sections' -> 'input' is empty. You must add at least 1 section to a form.");
        }
    }

    private bool IsFormRegistered(string formId)
    {
        var form = _registeredForms.FirstOrDefault(c => c.Id == formId);

        if (form is null)
            return false;
        return false;
    }
}
