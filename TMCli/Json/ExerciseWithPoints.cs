using System;
using System.Collections.Generic;

namespace TMCli.Json
{
    public class ExerciseWithPoints
    {
        public int Id { get; set; }
        public IEnumerable<AvailablePoint> AvailablePoints { get; set; } = default!;
        public IEnumerable<string> AwardedPoints { get; set; } = default!;
        public string Name { get; set; } = default!;

        public DateTime? PublishTime { get; set; }
        public DateTime? SolutionvisibleAfter { get; set; }

        public DateTime? Deadline { get; set; }
        public DateTime? SoftDeadline { get; set; }
        
        public bool Disabled { get; set; }
        public bool Unlocked { get; set; }
    }
}