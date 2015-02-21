using System;

namespace EF.Models
{
    public class GameTask
    {
        public Guid Id { get; set; }
        public TaskType Type { get; set; }
        public string Text { get; set; }
    }

    public enum TaskType
    {
        CrawlLine = 0,
    }
}