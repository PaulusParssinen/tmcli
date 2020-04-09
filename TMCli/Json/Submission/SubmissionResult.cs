using System;
using System.Collections.Generic;

namespace TMCli.Json.Submission
{
    public class SubmissionResult
    {
        public int ApiVersion { get; set; }
        
        public bool AllTestsPassed { get; set; }
        public int UserId { get; set; }
        public string Error { get; set; }
        public string Course { get; set; }
        public string ExerciseName { get; set; }
        
        public SubmissionStatus Status { get; set; }
        public string SubmissionUrl { get; set; }
        
        public IList<string> Points { get; set; }
        
        public int ProcessingTime { get; set; }
        public string MessageForPaste { get; set; }

        public IList<string> MissingReviewPoints { get; set; }
        public IList<TestResult> TestCases { get; set; }
        public IList<FeedbackQuestion> FeedbackQuestions { get; set; }
        public string FeedbackAnswerUrl { get; set; }
        public string SolutionsUrl { get; set; }
        public string Valgrind { get; set; }
        public bool Reviewed { get; set; }
        public bool RequestsReview { get; set; }
        public DateTime SubmittedAt { get; set; }

        //validationResult
    }
}
