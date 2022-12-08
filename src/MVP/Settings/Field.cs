namespace MVP.Settings;

public class Field
{
    public string Name { get; set; } = null!;
    public object Type { get; set; } = null!;
    public bool Required { get; set; } = default;
}
