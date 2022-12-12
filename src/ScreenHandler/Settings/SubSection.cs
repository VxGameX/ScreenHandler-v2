namespace ScreenHandler.Settings;

public class SubSection
{
    public string? Id { get; set; }
    public string? ActivationAnswer { get; set; }
    public IEnumerable<Section>? Sections { get; set; }
}
