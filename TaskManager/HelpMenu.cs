using System;

namespace TaskManager
{
    public static class HelpMenu
    {
        public static void Help()
        {
            Console.Clear();
            MainMenu.Menu();
            ConsoleEx.WriteLine(" :: HELP ::", ConsoleColor.DarkCyan);
            ConsoleEx.WriteLine("".PadLeft(115, '^'), ConsoleColor.DarkCyan);
            Console.WriteLine(">>> Available commands: ");
            ConsoleEx.WriteLine("".PadLeft(115, '-'), ConsoleColor.DarkCyan);
            ConsoleEx.WriteLine(":: add ::\nadd a new task", ConsoleColor.Gray);
            Console.WriteLine();
            ConsoleEx.WriteLine(":: remove ::\ndelete a task", ConsoleColor.Gray);
            Console.WriteLine();
            ConsoleEx.WriteLine(":: show ::\nshow all tasks", ConsoleColor.Gray);
            Console.WriteLine();
            ConsoleEx.WriteLine(":: sort ::\nfilter tasks according to data type", ConsoleColor.Gray);
            Console.WriteLine();
            ConsoleEx.WriteLine(":: save ::\nsave all tasks to a file [Data.csv]", ConsoleColor.Gray);
            Console.WriteLine();
            ConsoleEx.WriteLine(":: load ::\nload tasks from a file", ConsoleColor.Gray);
            Console.WriteLine();
            ConsoleEx.WriteLine(":: exit ::\nexit from the task manager", ConsoleColor.Gray);
            Console.WriteLine();
            ConsoleEx.WriteLine("".PadLeft(115, '='), ConsoleColor.DarkCyan);
            Console.Write("Press ENTER to continue... ");
            Console.ReadLine();
        }
    }
}