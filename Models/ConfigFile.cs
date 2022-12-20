using Newtonsoft.Json;

namespace ScreenHandler.Models;

public class ConfigFile
{
    [JsonProperty(Order = 1)]
    public string Id { get; set; } = null!;

    [JsonProperty(Order = 2)]
    public string Type { get; set; } = null!;

    [JsonProperty(Order = 3)]
    public Title Title { get; set; } = null!;

    [JsonProperty(Order = 4)]
    public Body Body { get; set; } = null!;
}
