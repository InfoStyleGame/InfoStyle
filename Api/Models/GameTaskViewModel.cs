using System;
using EF.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Api.Models
{
    public class GameTaskViewModel
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TaskType Type { get; set; }

        public Phrase[] Data { get; set; }
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