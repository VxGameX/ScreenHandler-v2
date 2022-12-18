using ScreenHandler.Models;

namespace ScreenHandler.Handlers;

public interface IFormHandlerBuilder<TEntity>
{
    Form Form { get; set; }
    IFormHandler<TEntity> Build();
    IFormHandlerBuilder<TEntity> RegisterSection(string sectionId);
}
