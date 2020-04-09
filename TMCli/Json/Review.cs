using System;
using System.Collections.Generic;

namespace TMCli.Json
{
    public class Review
    {
        public int SubmissionId { get; set; }
        public string ExeciseName { get; set; }
        public int Id { get; set; }
        
        public bool MarkedAsRead { get; set; }
        
        public string ReviewerName { get; set; }
        public string ReviewBody { get; set; }

        public IList<string> Points { get; set; }
        public IList<string> PointsNotAwarded { get; set; }

        public string Url { get; set; }
        public string UpdateUrl { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
