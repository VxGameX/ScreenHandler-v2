namespace ScreenHandler.Models;

public class Input
{
    public string Type { get; set; } = null!;
    public IList<string>? Options { get; set; }
}
