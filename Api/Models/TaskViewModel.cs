using System;
using EF.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Api.Models
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TaskType Type { get; set; }

        public TaskData Data { get; set; }
    }

    public class TaskData
    {
        public Phrase[] Text { get; set; }
    }

    public class Phrase
    {
        public string Text { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TextType Type { get; set; }
    }

    public enum TextType
    {
        Normal,
        Stop
    }
}