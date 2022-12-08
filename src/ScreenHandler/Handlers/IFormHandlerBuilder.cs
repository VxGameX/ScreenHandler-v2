namespace ScreenHandler.Handlers;

public interface IFormHandlerBuilder
{
    IFormHandlerBuilder RegisterFormSection(string sectionId);
    IFormHandlerBuilder RegisterFormSection(IEnumerable<string> sectionsId);
    IFormHandlerBuilder RegisterFormSection(params string[] sectionsId);
}