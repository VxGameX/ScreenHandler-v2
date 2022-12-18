namespace ScreenHandler.Models;

public class Title
{
    public string Label { get; set; } = null!;
    public bool Centralized { get; set; } = default;
    public string? ForegroundColor { get; set; }
    public string? BackgroundColor { get; set; }
}
