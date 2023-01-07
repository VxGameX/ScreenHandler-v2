namespace ConsoleScreenHandler.Validators;

public interface IValidator<TEntity>
{
    void Register(TEntity entity);
    void RunValidations(TEntity entity);
}
