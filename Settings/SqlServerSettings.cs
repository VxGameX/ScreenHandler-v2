namespace ConsoleScreenHandler.Settings;

public class SqlServerSettings
{
    public string Server { get; init; } = null!;
    public string Database { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string ConnectionString => $"Server={Server}; Database={Database}; User Id={Username}; Password={Password};";
}
