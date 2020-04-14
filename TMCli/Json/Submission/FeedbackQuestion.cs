namespace TMCli.Json.Submission
{
    public class FeedbackQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; } = default!;
        public string Kind { get; set; } = default!;
    }
}
