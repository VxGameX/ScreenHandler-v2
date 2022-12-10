namespace ScreenHandler.Settings;

public class Input
{
    public string Type { get; set; } = null!;
    public IList<string>? Options { get; set; }
    public ICollection<int>? SelectedOptions { get; set; }
    public string? Answer { get; set; }
}
