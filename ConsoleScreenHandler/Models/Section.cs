namespace ConsoleScreenHandler.Models;

public class Section
{
    public string Id { get; set; } = null!;
    public string Label { get; set; } = null!;
    public string Input { get; set; } = null!;
    public bool Required { get; set; }
}
