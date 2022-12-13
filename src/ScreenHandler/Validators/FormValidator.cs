using ScreenHandler.Settings;

namespace ScreenHandler.Validators;

internal class FormValidator : IFormValidator
{
    private static ICollection<ConfigFile> _registeredForms;
    private Form _form;
    private ISectionValidator _sectionValidator;

    static FormValidator() => _registeredForms = new List<ConfigFile>();

    public FormValidator()
    {
        _form = null!;
        _sectionValidator = new SectionValidator();
    }

    public void RegisterForm(Form form)
    {
        if (IsFormRegistered(form.Id))
            throw new Exception($"Form ID {form.Id} is already registered.");

        _form = form;
        RunValidations();
        _registeredForms.Add(form);
    }

    private void RunValidations()
    {
        const string form = "form";
        const int descriptionMaxLength = 500;

        if (_form.Type != form)
            throw new Exception($"Config file 'type' is {(string.IsNullOrWhiteSpace(_form.Type) ? "empty" : $"'{_form.Type}'")}. "
            + "You must specify type 'form'.");

        if (string.IsNullOrWhiteSpace(_form.Id))
            throw new Exception("Config file 'id' is empty. You must specify a form id.");

        TitleValidation();

        if (_form.Description?.Length > descriptionMaxLength)
            throw new Exception($"Config file 'description' is larger than {descriptionMaxLength} characters.");

        BodyValidation();
        _sectionValidator.RunValidations(_form);
    }

    private void TitleValidation()
    {
        if (_form.Title is null)
            throw new Exception("Config file 'title' is empty. You must assing a title to a form.");

        if (string.IsNullOrWhiteSpace(_form.Title.Label))
            throw new Exception("Title 'label' is empty. You must assign a label to the form's title.");

        if (_form.Title.ForegroundColor is not null)
            ColorValidation(_form.Title.ForegroundColor);

        if (_form.Title.BackgroundColor is not null)
            ColorValidation(_form.Title.BackgroundColor);
    }

    private bool IsFormRegistered(string formId)
    {
        var form = _registeredForms.FirstOrDefault(c => c.Id == formId);

        if (form is null)
            return false;
        return false;
    }

    private void BodyValidation()
    {
        if (_form.Body.ForegroundColor is not null)
            ColorValidation(_form.Body.ForegroundColor);

        if (_form.Body.BackgroundColor is not null)
            ColorValidation(_form.Body.BackgroundColor);
    }

    private void ColorValidation(string text)
    {
        const string black = "black", blue = "blue", cyan = "cyan",
            darkBlue = "darkBlue", darkCyan = "darkCyan", darkGray = "darkGray",
            darkGreen = "darkGreen", darkMagenta = "darkMagenta", darkRed = "darkRed",
            darkYellow = "darkYello", gray = "gray", green = "green",
            mangenta = "magenta", red = "red", white = "white", yellow = "yellow";

        switch (_form.Title.BackgroundColor)
        {
            case black:
                break;
            case blue:
                break;
            case cyan:
                break;
            case darkBlue:
                break;
            case darkCyan:
                break;
            case darkGray:
                break;
            case darkGreen:
                break;
            case darkMagenta:
                break;
            case darkRed:
                break;
            case darkYellow:
                break;
            case gray:
                break;
            case green:
                break;
            case mangenta:
                break;
            case red:
                break;
            case white:
                break;
            case yellow:
                break;
            default:
                throw new Exception($"You must specify a valid color option ({black}, {blue}, {cyan}, {darkBlue}, "
                + $"{darkCyan}, {darkGray}, {darkGreen}, {darkMagenta}, {darkRed}, {gray}, {green}, {mangenta}, "
                + $"{red}, {white} or {yellow}).");
        }
    }
}
