namespace ScreenHandler.Settings;

public class Input
{
    public string Type { get; set; } = null!;
    public IEnumerable<string>? options { get; set; }
}
