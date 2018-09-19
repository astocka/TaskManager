using System;

namespace TaskManager
{
    public class MainMenu
    {
        public static void Menu()
        {
            Console.WriteLine();
            Console.WriteLine();
            ConsoleEx.WriteLine("".PadLeft(115, '='), ConsoleColor.DarkCyan);
            ConsoleEx.WriteLine(":: T A S K  M A N A G E R ::".PadLeft(70), ConsoleColor.DarkCyan);
            ConsoleEx.WriteLine("".PadLeft(115, '-'), ConsoleColor.DarkCyan);
            ConsoleEx.Write(" add ".PadLeft(30), ConsoleColor.DarkGreen);
            Console.Write("::");
            ConsoleEx.Write(" remove ", ConsoleColor.DarkRed);
            Console.Write("::");
            ConsoleEx.Write(" show ", ConsoleColor.DarkYellow);
            Console.Write("::");
            ConsoleEx.Write(" sort ", ConsoleColor.DarkMagenta);
            Console.Write("::");
            ConsoleEx.Write(" save ", ConsoleColor.DarkGray);
            Console.Write("::");
            ConsoleEx.Write(" load ", ConsoleColor.Gray);
            Console.Write("::");
            ConsoleEx.Write(" exit ", ConsoleColor.Blue);
            Console.Write("::");
            ConsoleEx.WriteLine(" help ", ConsoleColor.Blue);
            ConsoleEx.WriteLine("".PadLeft(115, '='), ConsoleColor.DarkCyan);
        }
    }
}