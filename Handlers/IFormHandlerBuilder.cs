using ScreenHandler.Configurators;
using ScreenHandler.Models;

namespace ScreenHandler.Handlers;

public interface IFormHandlerBuilder
{
    ICollection<Section> FormSections { get; set; }
    IFormHandler Build();
    ISectionConfigurator SectionsSettings();
}
