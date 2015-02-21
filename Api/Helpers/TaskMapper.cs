using System.Linq;
using Api.Models;
using EF;

namespace Api.Helpers
{
    public class TaskMapper
    {
        public TaskViewModel Parse(Task task)
        {
            return new TaskViewModel
            {
                Id = task.Id,
                Type = task.Type,
                Data = Parse(task.Text)
            };
        }

        private static Phrase[] Parse(string text)
        {
            var tokens = text.Split('{', '}');
            return tokens.Select((t, i) => new Phrase {Text = t, Type = i%2 == 0 ? TextType.Normal : TextType.Stop})
                    .Where(p => !string.IsNullOrEmpty(p.Text))
                    .ToArray();
        }
    }
}