namespace ScreenHandler.Models;

public class ScreenDefinition
{
    public string Id { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Title { get; set; } = null!;
    public ICollection<Section> Sections { get; set; } = null!;
}
