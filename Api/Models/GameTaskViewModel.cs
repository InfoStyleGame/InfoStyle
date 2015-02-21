using System;
using EF.Models;

namespace Api.Models
{
    public class GameTaskViewModel
    {
        public Guid Id { get; set; }
        public TaskType Type { get; set; }
        public Phrase[] Text { get; set; }
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