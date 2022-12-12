namespace ScreenHandler.Settings;

public class ConfigFile
{
    public string Type { get; set; } = null!;
    public string Id { get; set; } = null!;
    public Title Title { get; set; } = null!;
    public Body Body { get; set; } = null!;
    public string? Description { get; set; }
    public IEnumerable<Section>? Sections { get; set; }
    public IEnumerable<Action> Actions { get; set; } = null!;
}
