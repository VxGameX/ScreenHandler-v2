namespace ScreenHandler.Models;

public interface IEntity<TIdentifier>
    where TIdentifier : struct
{
    TIdentifier Id { get; set; }
}
