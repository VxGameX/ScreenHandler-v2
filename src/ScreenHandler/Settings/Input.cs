namespace ScreenHandler.Settings;

public class Input
{
    public string Type { get; set; } = null!;
    public IEnumerable<string>? Options { get; set; }
    public IEnumerable<int>? SelectedOptions { get; set; }
}
