namespace ScreenHandler.Helpers;

public static class HandlerHelpers
{
    public static string ScreenTitle { get; set; } = null!;

    public static void ClearScreen()
    {
        Console.ResetColor();
        Console.Clear();
        ShowTitle();
    }

    private static void ShowTitle() => Console.WriteLine($"{ScreenTitle}{Environment.NewLine}");

    public static void Pause() => Console.ReadKey(true);
}
