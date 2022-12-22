namespace ScreenHandler.Handlers;

public class HandlerResponse : IResponse
{
    public bool Success { get; set; }
    public IDictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
}
