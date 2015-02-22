namespace Api.Models
{
    public class DailyCompareViewModel
    {
        public int TaskId { get; set; }
        public string InitialText { get; set; }
        public Competitor[] Competitors { get; set; }
    }

    public class Competitor
    {
        public int Id { get; set; }
        public string Text { get; set; } 
    }
}