using ScreenHandler.Models;

namespace ScreenHandler.Handlers;

public interface IFormHandlerBuilder
{
    Form Form { get; set; }
    IFormHandler Build();
}
