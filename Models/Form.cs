using Newtonsoft.Json;

namespace ScreenHandler.Models;

public class Form : ConfigFile
{
    [JsonProperty(Order = 5)]
    public string? Description { get; set; }

    [JsonProperty(Order = 6)]
    public ICollection<Section> Sections { get; set; } = null!;
}
