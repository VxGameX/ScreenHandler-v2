namespace ScreenHandler.Models;

public class Answer
{
    public string Field { get; set; } = null!;
    public string? Response { get; set; }
    public ICollection<string>? SelectedOptions { get; set; }
}
