namespace Api.Models
{
    public class DailyTextViewModel
    {
        public DailyTextViewModel(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; set; }
        public string Text { get; set; }
    }
}