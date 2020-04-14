using System;
using System.Collections.Generic;

using TMCli.Json.Submission;

namespace TMCli.Json
{
    public class CoreExercise
    {
        public string CourseName { get; set; } = default!;
        public int CourseId { get; set; }

        public bool CodeReviewRequestsEnabled { get; set; }
        public bool RunTestsLocallyActionEnabled { get; set; }

        public string ExerciseName { get; set; } = default!;
        public int ExerciseId { get; set; }

        public DateTime UnlockedAt { get; set; }
        public DateTime Deadline { get; set; }

        public IEnumerable<CoreSubmission> Submissions { get; set; } = default!;
    }
}
