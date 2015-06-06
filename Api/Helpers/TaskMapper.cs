using System.Linq;
using Api.Controllers;
using Api.Models;

namespace Api.Helpers
{
    public class TaskMapper
    {
        public TaskViewModel Parse(Task task)
        {
            return new TaskViewModel
            {
                Id = task.Id,
                Data = new TaskData {Text = Parse(task.Text)},
            };
        }

        private static Phrase[] Parse(string text)
        {
            var tokens = text.Split('{', '}');
            return tokens.Select((t, i) => new Phrase {Text = t, Type = i%2 == 0 ? TextType.Normal : ParseMarked(t)})
                    .Where(p => !string.IsNullOrEmpty(p.Text))
                    .ToArray();
        }

        private static TextType ParseMarked(string token)
        {
            return TextType.Stop;
        }
    }
}