﻿using System;
using System.Collections.Generic;

namespace TMCli.Json
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Locked { get; set; }

        public string DeadlineDescription { get; set; }
        public DateTime Deadline { get; set; }
        
        public string SoftDeadlineDescription { get; set; }
        public DateTime SoftDeadline { get; set; }

        public string Checksum { get; set; }
        
        public string ZipUrl { get; set; }
        public string ReturnUrl { get; set; }
        public string SolutionsZipUrl { get; set; }

        public bool Returnable { get; set; }
        public bool RequiresReview { get; set; }
        public bool Attempted { get; set; }
        public bool Completed { get; set; }
        public bool Reviewed { get; set; }
        public bool AllReviewPointsGiven { get; set; }

        public string MemoryLimit { get; set; }
        public IList<string> RuntimeParams { get; set; }
        public ValgrindStrategy ValgrindStrategy { get; set; } //fail
        public bool CodeReviewRequestsEnabled { get; set; } //true
        public bool RunTestsLocallyActionEnabled { get; set; } //true
        public string ExerciseSubmissionUrl { get; set; }
    }
}