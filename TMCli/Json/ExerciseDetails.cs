using System;
using System.Collections.Generic;

namespace TMCli.Json
{
    public class ExerciseDetails
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Locked { get; set; }

        public string? DeadlineDescription { get; set; }
        public DateTime? Deadline { get; set; }
        public string? SoftDeadlineDescription { get; set; }
        public DateTime? SoftDeadline { get; set; }

        public string Checksum { get; set; } = default!;

        public string ReturnUrl { get; set; } = default!;
        public string ZipUrl { get; set; } = default!;

        public bool Returnable { get; set; }
        public bool RequiresReview { get; set; }
        public bool Attempted { get; set; }
        public bool Completed { get; set; }
        public bool Reviewed { get; set; }
        public bool? AllReviewPointsGiven { get; set; }

        public string MemoryLimit { get; set; } = default!;
        public IList<string> RuntimeParams { get; set; } = default!;
        public ValgrindStrategy ValgrindStrategy { get; set; } 
        
        public bool CodeReviewRequestsEnabled { get; set; }
        public bool RunTestsLocallyActionEnabled { get; set; }
        
        public string ExerciseSubmissionUrl { get; set; } = default!;

        public string? LatestSubmissionUrl { get; set; }
        public int? LatestSubmissionId { get; set; }

        public string? SolutionsZipUrl { get; set; }
    }
}
