namespace MVP.Settings;

public class ConfigFile
{
    public string Id { get; set; } = null!;
    public Title Title { get; set; } = null!;
    public IEnumerable<Field> Fields { get; set; } = null!;
    public IEnumerable<Action> Actions { get; set; } = null!;
}
