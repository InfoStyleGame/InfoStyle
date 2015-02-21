using System;

namespace Api.Models
{
    public class GameTask
    {
        public Guid Id { get; set; }
        public TaskType Type { get; set; }
        public Phrase[] Text { get; set; }
    }

    public enum TaskType
    {
        CrawlLine,
    }

    public class Phrase
    {
        public string Text { get; set; }
        public TextType Type { get; set; }
    }

    public enum TextType
    {
        Normal,
        Stop
    }
}