namespace ScreenHandler.Settings;

public class Section
{
    public string Id { get; set; } = null!;
    public string Label { get; set; } = null!;
    public Input input { get; set; } = null!;
    public bool Required { get; set; } = default;
}
