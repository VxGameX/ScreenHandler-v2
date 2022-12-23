namespace ConsoleScreenHandler.Handlers;

public interface IResult
{
    bool Success { get; set; }
    IDictionary<string, string> Data { get; set; }
}
