namespace ConsoleScreenHandler.Handlers;

public class HandlerResponse : IResponse
{
    public bool Success { get; set; }
    public IDictionary<string, string> Data { get; set; }

    public HandlerResponse(IDictionary<string, string> data)
    {
        Data = data;
    }
}
