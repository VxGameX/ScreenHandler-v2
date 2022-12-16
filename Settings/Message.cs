using Newtonsoft.Json;

namespace ScreenHandler.Settings;

public class Message : ConfigFile
{
    [JsonProperty(Order = 5)]
    public string Content { get; set; } = null!;
}
