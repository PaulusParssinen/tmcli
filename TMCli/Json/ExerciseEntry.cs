namespace TMCli.Json
{
    /// <summary>
    /// Represents a course - exercise name pair.
    /// </summary>
    public class ExerciseEntry
    {
        public string CourseName { get; set; }
        public string ExerciseName { get; set; }

        //TODO: Deserialize from / delimited str
    }
}
