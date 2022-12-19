namespace ScreenHandler.Models;

public class Answer
{
    public ICollection<string>? SelectedOptions { get; set; }
    public string? Response { get; set; }
}