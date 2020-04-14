using System;
using System.Collections.Generic;

namespace TMCli.Json
{
    public class Review
    {
        public int SubmissionId { get; set; }
        public string ExeciseName { get; set; } = default!;
        public int Id { get; set; }
        
        public bool MarkedAsRead { get; set; }
        
        public string ReviewerName { get; set; } = default!;
        public string ReviewBody { get; set; } = default!;

        public IList<string> Points { get; set; } = default!;
        public IList<string> PointsNotAwarded { get; set; } = default!;

        public string Url { get; set; } = default!;
        public string UpdateUrl { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
