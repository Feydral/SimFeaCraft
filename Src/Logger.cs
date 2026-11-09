namespace SimFeaCraft;

public static class Logger
{
    private static void Log(string sender, string type, string message, ConsoleColor color, bool colorMessage = false)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss");

        Console.ForegroundColor = color;
        Console.Write($"[{timestamp}] [{sender}/{type}]: ");

        Console.ForegroundColor = colorMessage ? ConsoleColor.White : color;
        Console.WriteLine(message);

        Console.ResetColor();
    }

    public static void Info(string sender, string message)
    {
        Log(sender, "INFO", message, ConsoleColor.Blue, true);
    }

    public static void Warning(string sender, string message)
    {
        Log(sender, "WARN", message, ConsoleColor.Yellow);
    }

    public static void Error(string sender, string message)
    {
        Log(sender, "ERROR", message, ConsoleColor.Red);
    }
}
