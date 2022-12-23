namespace ConsoleScreenHandler.Handlers;

public interface IResponse
{
    bool Success { get; set; }
    IDictionary<string, string> Data { get; set; }
}
