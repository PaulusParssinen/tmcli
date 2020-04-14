using System;

namespace TMCli.Json.Submission
{
    public class CoreSubmission
    {
        public string ExerciseName { get; set; } = default!;
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public bool AllTestsPassed { get; set; }
        public string SubmittedZipUrl { get; set; } = default!;
        public string Points { get; set; } = default!;
        public bool Reviewed { get; set; }
        public bool RequestsReview { get; set; }
        public int ProcessingTime { get; set; }
    }
}
