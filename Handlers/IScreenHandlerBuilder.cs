using ScreenHandler.Models;

namespace ScreenHandler.Handlers;

public interface IScreenHandlerBuilder
{
    ScreenDefinition Screen { get; set; }
    IScreenHandler Build();
}
