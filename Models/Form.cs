using Newtonsoft.Json;

namespace ScreenHandler.Models;

public class Form : ConfigFile
{
    [JsonProperty(Order = 4)]
    public string? Description { get; set; }

    [JsonProperty(Order = 5)]
    public ICollection<Section> Sections { get; set; } = null!;
}
