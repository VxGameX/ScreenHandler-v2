using Newtonsoft.Json;

namespace ScreenHandler.Settings;

public class Form : ConfigFile
{
    [JsonProperty(Order = 5)]
    public string? Description { get; set; }

    [JsonProperty(Order = 6)]
    public IEnumerable<Section> Sections { get; set; } = null!;
}
