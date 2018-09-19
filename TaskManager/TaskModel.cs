using System;
using System.Text;

namespace TaskManager
{
    public class TaskModel
    {
        public string Description { get; protected set; }
        public DateTime StartTime { get; protected set; }
        public DateTime? EndTime { get; protected set; }
        public bool? AllDayTask { get; protected set; }
        public bool? ImportantTask { get; protected set; }

        public TaskModel(string description, DateTime startTime, DateTime? endTime, bool? allDayTask, bool? importantTask)
        {
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            AllDayTask = allDayTask;
            ImportantTask = importantTask;

            if (endTime == null && allDayTask == true)
            {
                EndTime = StartTime;
            }
        }

        public string ExportToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Description: [{Description}],\n");
            stringBuilder.Append($"Start date: [{StartTime}],\n");
            stringBuilder.Append($"End date: [{EndTime}],\n");
            stringBuilder.Append($"All-day task: [{AllDayTask}],\n");
            stringBuilder.Append($"Important task: [{ImportantTask}]");
            return stringBuilder.ToString();
        }

        public string ExportToCsvFile()
        {
            return $"{Description},{StartTime},{EndTime},{AllDayTask},{ImportantTask}";
        }
    }
}