using ConsoleScreenHandler.Exceptions;
using ConsoleScreenHandler.Models;
using Microsoft.Extensions.Logging;

namespace ConsoleScreenHandler.Validators;

public class ScreenValidator : IValidator<Screen>
{
    private readonly ILogger<ScreenValidator> _logger;
    private ICollection<Screen> _registeredScreens;

    public ScreenValidator(ILogger<ScreenValidator> logger, ICollection<Screen> registeredScreens)
    {
        _logger = logger;
        _registeredScreens = registeredScreens;
    }

    public void Register(Screen screen)
    {
        RunValidations(screen);
        _registeredScreens.Add(screen);
    }

    private void RunValidations(Screen form)
    {
        if (IsScreenRegistered(form.Id))
            throw new ScreenStructException($"Form ID {form.Id} is already registered.");

        if (string.IsNullOrWhiteSpace(form.Id))
            throw new ScreenStructException("Config file 'id' is empty. You must specify a form id.");

        if (string.IsNullOrWhiteSpace(form.Title))
            throw new ScreenStructException("Config file 'title' is empty. You must assing a title to a form");

        if (!form.Sections.Any())
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

    private bool IsScreenRegistered(string screenId)
    {
        var form = _registeredScreens.FirstOrDefault(c => c.Id == screenId);
        return form is not null;
    }
}
