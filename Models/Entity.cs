namespace ScreenHandler.Models;

public class Entity<TIdentifier> : IEntity<TIdentifier>
    where TIdentifier : struct
{
    public TIdentifier Id { get; set; }
}
