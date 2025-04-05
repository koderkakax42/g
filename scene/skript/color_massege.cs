using Godot;
using System;

namespace color_massege
{
public static class color_massege
{
    public static void Error(string message)
    {
        GD.PrintErr($"🔴 {message}"); // Красный в некоторых терминалах
    }
    
    public static void Error(params object[] message)
    {
      // GD.PrintErr(message );
         GD.PrintErr($"🔴 {message}");
    }

    public static void Warning(string message)
    {
        GD.Print($"🟡 {message}"); // Жёлтый (можно добавить ANSI-код)
    }

    public static void Info(string message)
    {
        GD.Print($"🔵 {message}");
    }
}
}
// Использование:
