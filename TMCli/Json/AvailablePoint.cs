namespace TMCli.Json
{
    public class AvailablePoint
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public string Name { get; set; } = default!;
        public bool RequiresReview { get; set; }
    }
}
