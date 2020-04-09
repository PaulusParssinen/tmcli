using System;

namespace TMCli.Json.Submission
{
    public class Submission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? PretestError { get; set; }
        public DateTime CreateddAt { get; set; }
        public string ExerciseName { get; set; }
        public int CourseId { get; set; }
        
        public bool Processed { get; set; }
        public bool AllTestsPassed { get; set; }
        public string Points { get; set; }

        public DateTime ProcessingTriedAt { get; set; }
        public DateTime ProcessingBeganAt { get; set; }
        public int TimesSentToSandbox { get; set; }
        public DateTime ProcessingAttemptsStartedAt { get; set; }
        
        public bool RequiresReview { get; set; }
        public bool RequestsReview { get; set; }
        public bool Reviewed { get; set; }
        public string MessageForReviewer { get; set; }
        public bool NewerSubmissionReviewed { get; set; }
        public bool ReviewDismissed { get; set; }

        public bool PasteAvailable { get; set; }
        public string MessageForPaste { get; set; }
        public string? PasteKey { get; set; }
    }
}
