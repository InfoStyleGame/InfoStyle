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
                    new Phrase {Text = "��� � �������. ", Type = TextType.Normal},
                    new Phrase {Text = "����!", Type = TextType.Stop},
                    new Phrase {Text = " � ����� ��� � �������.", Type = TextType.Normal}
                }
            };
        }
    }
}