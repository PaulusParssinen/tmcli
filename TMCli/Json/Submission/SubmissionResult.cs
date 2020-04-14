using System;
using System.Collections.Generic;

namespace TMCli.Json.Submission
{
    public class SubmissionResult
    {
        public int ApiVersion { get; set; }
        
        public bool AllTestsPassed { get; set; }
        public int UserId { get; set; }
        public string Login { get; set; } = default!;
        public string Course { get; set; } = default!;
        public string ExerciseName { get; set; } = default!;

        public SubmissionStatus Status { get; set; }
        public string SubmissionUrl { get; set; } = default!;

        public IList<string> Points { get; set; } = default!;

        public string? SolutionsUrl { get; set; }
        public DateTime SubmittedAt { get; set; }
        public int ProcessingTime { get; set; }
        public string MessageForPaste { get; set; } = default!;

        public IList<string> MissingReviewPoints { get; set; } = default!;

        public string? Valgrind { get; set; }
        public bool? Reviewed { get; set; }
        public bool? RequestsReview { get; set; }

        public int SubmissionsBeforeThis { get; set; }
        public int TotalUnprocessed { get; set; }
        
        public IList<TestResult> TestCases { get; set; } = default!;

        public IList<FeedbackQuestion> FeedbackQuestions { get; set; } = default!;
        public string FeedbackAnswerUrl { get; set; } = default!;

        public string Error { get; set; } = default!;
    }
}
