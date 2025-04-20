using System;

public class Logger
{
    public static void LogInfo(string str)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("INFO");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("] " + str + "\r\n");
    }

    public static void LogError(string str)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("ERROR");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("] " + str + "\r\n");
    }

    public static void LogWarning(string str)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("WARNING");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("] " + str + "\r\n");
    }

    public static void LogSuccess(string str)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("SUCCESS");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("] " + str + "\r\n");
    }
}