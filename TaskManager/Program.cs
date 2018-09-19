using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaskManager
{
    class Program
    {
        public static List<TaskModel> TaskList { get; private set; } = new List<TaskModel>();

        static void Main(string[] args)
        {
            string command = "";

            do
            {
                Console.Clear();
                MainMenu.Menu();
                Console.Write("Enter the command: ");
                command = Console.ReadLine().Trim().ToLower();

                if (command == "exit")
                    break;

                switch (command)
                {
                    case "add":
                        AddTask(TaskList);
                        break;
                    case "remove":
                        RemoveTask(TaskList);
                        break;
                    case "show":
                        ShowTasks(TaskList);
                        break;
                    case "sort":
                        SortTasks(TaskList);
                        break;
                    case "save":
                        SaveTasks(TaskList);
                        break;
                    case "load":
                        LoadTasks(TaskList);
                        break;
                    case "help":
                        HelpMenu.Help();
                        break;

                    default:
                        ConsoleEx.Write("Wrong command.", ConsoleColor.Red);
                        Console.ReadKey();
                        break;
                }
            }
            while (true);
        }

        // AddTask method take data from user and create new TaskModel
        private static TaskModel CreateTask()
        {
            Console.Clear();
            MainMenu.Menu();

            string description = "";
            DateTime startTime = DateTime.Now;
            DateTime? endTime = null;
            bool? allDayTask = false;
            bool? importantTask = false;

            ConsoleEx.WriteLine(":: NEW TASK ::", ConsoleColor.DarkCyan);
            ConsoleEx.WriteLine("".PadLeft(115, '^'), ConsoleColor.DarkCyan);
            Console.WriteLine();

            Console.Write("Enter description [required]: ");
            try
            {
                description = Console.ReadLine().Trim();
                if (description == "")
                {
                    ConsoleEx.WriteLine("Missing task description.", ConsoleColor.Red);
                    Console.ReadLine();
                    Console.Clear();
                    CreateTask();
                }
            }
            catch (NullReferenceException)
            {
                ConsoleEx.WriteLine("Missing task description.", ConsoleColor.Red);

            }
            catch (Exception)
            {
                ConsoleEx.WriteLine("Missing task description.", ConsoleColor.Red);
            }

            Console.Write("Enter start time [required, in format: [YYYY-MM-DD]: ");
            try
            {
                string checkStartDate = Console.ReadLine().Trim();
                if (checkStartDate.Length == 10 && checkStartDate[4] == '-' && checkStartDate[7] == '-')
                {
                    startTime = DateTime.Parse(checkStartDate); // set StartTime
                }
                else
                {
                    ConsoleEx.Write("Incorrect date format.", ConsoleColor.Red);
                    Console.ReadLine();
                    Console.Clear();
                    CreateTask();
                }
            }
            catch (NullReferenceException)
            {
                ConsoleEx.Write("Wrong data format.", ConsoleColor.Red);
            }
            catch (ArgumentOutOfRangeException)
            {
                ConsoleEx.Write("Wrong data format.", ConsoleColor.Red);
                return null;
            }
            catch (FormatException)
            {
                ConsoleEx.Write("Incorrect date format.", ConsoleColor.Red);
                return null;
            }
            catch (Exception)
            {
                ConsoleEx.Write("Wrong data format.", ConsoleColor.Red);
            }

            Console.Write("Enter end time [optional, in format: [YYYY-MM-DD]: ");
            string checkEndDate = Console.ReadLine().Trim();
            if (!string.IsNullOrWhiteSpace(checkEndDate)) // EndTime is optional - set null if it is empty
            {
                try
                {
                    if (checkEndDate.Length == 10 && checkEndDate[4] == '-' && checkEndDate[7] == '-')
                    {
                        endTime = DateTime.Parse(checkEndDate); // set EndTime
                    }
                    else
                    {
                        ConsoleEx.Write("Incorrect date format.", ConsoleColor.Red);
                    }
                }
                catch (Exception)
                {
                    ConsoleEx.Write("Incorrect date format.", ConsoleColor.Red);
                    return null;
                }
            }

            Console.Write("Is it all-day task? [optional, enter true or false]: ");
            string checkAllDayTask = Console.ReadLine().Trim();
            if (checkAllDayTask == "true") // set AllDayTask flag
            {
                allDayTask = true;
            }
            else if (checkAllDayTask == "false")
            {
                allDayTask = false;
            }
            else
            {
                allDayTask = null;
            }

            Console.Write("Is it important task? [optional, enter true or false]: ");
            string checkImportantTask = Console.ReadLine().Trim();
            if (checkImportantTask == "true") // set ImportantTask flag
            {
                importantTask = true;
            }
            else if (checkImportantTask == "false")
            {
                importantTask = false;
            }
            else
            {
                importantTask = null;
            }
            return new TaskModel(description, startTime, endTime, allDayTask, importantTask);
        }

        // AddTask method add new TaskModel object to taskList
        private static void AddTask(List<TaskModel> taskList)
        {
            var newTask = CreateTask();
            if (newTask != null)
            {
                taskList.Add(newTask);
                ConsoleEx.WriteLine("\nThe task was successfully added.", ConsoleColor.Green);
                Console.Write("Press ENTER to continue... ");
                Console.ReadLine();
            }
        }

        // RemoveTask method delete a task
        private static void RemoveTask(List<TaskModel> taskList)
        {
            int taskIndex = 0;

            Console.Clear();
            MainMenu.Menu();
            ConsoleEx.WriteLine(" :: REMOVE TASK ::", ConsoleColor.DarkCyan);
            ConsoleEx.WriteLine("".PadLeft(115, '^'), ConsoleColor.DarkCyan);
            ConsoleEx.WriteLine(">>> List of tasks:", ConsoleColor.Gray);
            ConsoleEx.WriteLine("".PadLeft(115, '-'), ConsoleColor.DarkCyan);

            for (int i = 0; i < taskList.Count; i++) // shows all tasks from the taskList
            {
                ConsoleEx.WriteLine($"Task number {i + 1}:", ConsoleColor.DarkRed);
                Console.WriteLine($"{taskList[i].ExportToString()}");
                Console.WriteLine();
            }
            ConsoleEx.WriteLine("".PadLeft(115, '='), ConsoleColor.DarkCyan);
            Console.WriteLine();

            Console.Write("Enter the index of the task to delete: ");
            //taskIndex = int.Parse(Console.ReadLine().Trim());
            try
            {
                taskIndex = int.Parse(Console.ReadLine().Trim());
                taskList.RemoveAt(taskIndex - 1);
                ConsoleEx.WriteLine($"Task number {taskIndex} has been deleted.", ConsoleColor.Green);
            }
            catch (FormatException)
            {
                ConsoleEx.Write("Wrong data format.", ConsoleColor.Red);
            }
            catch (IndexOutOfRangeException)
            {
                ConsoleEx.Write($"There is no #{taskIndex} task.", ConsoleColor.Red);
            }
            Console.Write("Press ENTER to continue... ");
            Console.ReadLine();
        }

        private static void SortTasks(List<TaskModel> taskList)
        {
            Console.Clear();
            MainMenu.Menu();
            ConsoleEx.WriteLine(" :: TASK FILTER ::", ConsoleColor.DarkCyan);
            ConsoleEx.WriteLine("".PadLeft(115, '^'), ConsoleColor.DarkCyan);
            Console.WriteLine(">>> Tasks filters: [1] Word, [2] Start date, [3] End date, [4] All-day tasks, [5] Important tasks");
            ConsoleEx.WriteLine("".PadLeft(115, '='), ConsoleColor.DarkCyan);
            Console.Write(" | Task ".PadRight(15));
            Console.Write(" | Start time ".PadLeft(39));
            Console.Write(" | End time ".PadLeft(20));
            Console.Write(" | All-day ".PadLeft(24));
            Console.WriteLine(" | Important ".PadLeft(15));
            ConsoleEx.WriteLine("".PadLeft(115, '='), ConsoleColor.DarkCyan);

            Filters.Filter();
            Console.WriteLine();
            ConsoleEx.WriteLine("".PadLeft(115, '-'), ConsoleColor.DarkCyan);
            Console.Write("ENTER to continue... ");
            Console.ReadKey();
        }

        //  ShowTasks method shows all added/uploaded tasks ordered by important tasks and start date
        private static void ShowTasks(List<TaskModel> taskList)
        {
            Console.Clear();
            MainMenu.Menu();
            ConsoleEx.WriteLine(" :: SUMMARY ::", ConsoleColor.DarkCyan);
            ConsoleEx.WriteLine("".PadLeft(115, '^'), ConsoleColor.DarkCyan);
            Console.Write(" | Task ".PadRight(15));
            Console.Write(" | Start time ".PadLeft(39));
            Console.Write(" | End time ".PadLeft(20));
            Console.Write(" | All-day ".PadLeft(24));
            Console.WriteLine(" | Important ".PadLeft(15));
            ConsoleEx.WriteLine("".PadLeft(115, '='), ConsoleColor.DarkCyan);

            var importantTasks = taskList.Where(a => a.ImportantTask == true).OrderBy(a => a.StartTime);
            foreach (TaskModel task in importantTasks)
            {
                Console.Write($" | {task.Description}".PadRight(40));
                Console.Write($" | {task.StartTime}");
                Console.Write($" | {task.EndTime}");
                Console.Write($" | {task.AllDayTask}".PadLeft(11));
                Console.Write($" | {task.ImportantTask}".PadLeft(13));
                Console.WriteLine();
            }

            var otherTasks = taskList.Where(a => a.ImportantTask == false || a.ImportantTask == null)
                    .OrderBy(a => a.StartTime);
            foreach (TaskModel task in otherTasks)
            {
                Console.Write($" | {task.Description}".PadRight(40));
                Console.Write($" | {task.StartTime}");
                Console.Write($" | {task.EndTime}");
                Console.Write($" | {task.AllDayTask}".PadLeft(11));
                Console.Write($" | {task.ImportantTask}".PadLeft(13));
                Console.WriteLine();
            }
            Console.WriteLine();
            ConsoleEx.WriteLine("".PadLeft(115, '-'), ConsoleColor.DarkCyan);

            Console.WriteLine();
            Console.Write("Press ENTER to continue... ");
            Console.ReadKey();
        }

        // SaveTasks method saves all tasks from taskList to a file Data.csv
        private static void SaveTasks(List<TaskModel> taskList)
        {
            List<string> tasksToString = new List<string>();

            Console.Clear();
            MainMenu.Menu();
            ConsoleEx.WriteLine(" :: SAVE TO A FILE :: ", ConsoleColor.DarkCyan);
            ConsoleEx.WriteLine("".PadLeft(115, '^'), ConsoleColor.DarkCyan);

            foreach (TaskModel task in taskList)
            {
                tasksToString.Add(task.ExportToCsvFile());
            }

            File.WriteAllLines("Data.csv", tasksToString);
            ConsoleEx.WriteLine(" >>> The tasks were successfully saved to [Data.csv] file.", ConsoleColor.Green);
            Console.WriteLine();
            Console.Write("Press ENTER to continue... ");
            Console.ReadKey();
        }

        // LoadTasks method loads all tasks form a file
        private static void LoadTasks(List<TaskModel> taskList)
        {
            string path = "";
            string[] listUpload = null;

            Console.Clear();
            MainMenu.Menu();
            ConsoleEx.WriteLine(" :: LOADING TASKS FROM A FILE :: ", ConsoleColor.DarkCyan);
            ConsoleEx.WriteLine("".PadLeft(115, '^'), ConsoleColor.DarkCyan);
            Console.WriteLine();
            Console.Write("Enter the file name: ");
            path = Console.ReadLine().Trim();

            if (File.Exists(path) == false)
            {
                ConsoleEx.Write("File was not found.", ConsoleColor.Red);
                Console.ReadLine();
                return;
            }

            string[] linesFromLoadedFile = File.ReadAllLines(path);
            List<string[]> tasksFromLoadedFile = new List<string[]>();
            char[] separators = { ',', ';' };

            foreach (string item in linesFromLoadedFile)
            {
                tasksFromLoadedFile.Add(item.Split(separators));
                try
                {
                    foreach (string[] loadedTask in tasksFromLoadedFile)
                    {
                        DateTime? endTime;
                        if (string.IsNullOrWhiteSpace(loadedTask[2]))
                        {
                            endTime = null;
                        }
                        else
                        {
                            endTime = DateTime.Parse(loadedTask[2]);
                        }

                        bool? allDayTask;
                        if (string.IsNullOrWhiteSpace(loadedTask[3]))
                        {
                            allDayTask = null;
                        }
                        else
                        {
                            allDayTask = bool.Parse(loadedTask[3]);
                        }

                        bool? importantTask;
                        if (string.IsNullOrWhiteSpace(loadedTask[4]))
                        {
                            importantTask = null;
                        }
                        else
                        {
                            importantTask = bool.Parse(loadedTask[4]);
                        }

                        taskList.Add(new TaskModel(loadedTask[0], DateTime.Parse(loadedTask[1]), endTime, allDayTask, importantTask));

                    }
                    tasksFromLoadedFile.Clear();
                }
                catch (FormatException)
                {
                    ConsoleEx.Write("Wrong file format.", ConsoleColor.Red);
                    Console.ReadLine();
                    return;
                }
                catch (Exception)
                {
                    ConsoleEx.Write("An unknown error occurred.", ConsoleColor.Red);
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine();
            ConsoleEx.WriteLine(" >>> Tasks from the file was successfully loaded.\n", ConsoleColor.Green);
            Console.Write("Press ENTER to continue... ");
            Console.ReadLine();
        }
    }
}

