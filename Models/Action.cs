namespace ScreenHandler.Models;

public class Action
{
    public string Name { get; set; } = null!;
    public char Button { get; set; }
    public string Handler { get; set; } = null!;
}
