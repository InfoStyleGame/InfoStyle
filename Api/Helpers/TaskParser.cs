using Api.Models;
using EF;

namespace Api.Helpers
{
    public class TaskParser
    {
        public TaskViewModel Parse(Task task)
        {
            return new TaskViewModel
            {
                Id = task.Id,
                Type = task.Type,
                Data = new[]
                {
                    new Phrase {Text = "Все в порядке. ", Type = TextType.Normal},
                    new Phrase {Text = "СТОП!", Type = TextType.Stop},
                    new Phrase {Text = " И снова все в порядке.", Type = TextType.Normal}
                }
            };
        }
    }
}