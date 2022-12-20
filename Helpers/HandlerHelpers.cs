namespace ScreenHandler.Helpers;

public static class HandlerHelpers
{
    private static string _screenTitle = "";

    public static void ClearScreen()
    {
        Console.ResetColor();
        Console.Clear();
        ShowTitle();
    }

    private static void ShowTitle() => Console.WriteLine($"{_screenTitle}{Environment.NewLine}");

    public static void Pause() => Console.ReadKey(true);
}
