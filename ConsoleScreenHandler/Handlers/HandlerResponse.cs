namespace ConsoleScreenHandler.Handlers;

public class HandlerResponse : IResult
{
    public bool Success { get; set; }
    public IDictionary<string, string> Data { get; set; }

    public HandlerResponse(IDictionary<string, string> data)
    {
        Data = data;
    }
}
