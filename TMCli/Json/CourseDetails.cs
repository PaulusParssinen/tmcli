using System.Collections.Generic;

namespace TMCli.Json
{
    public class CourseDetails
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;

        public string UnlockUrl { get; set; } = default!;
        public string DetailsUrl { get; set; } = default!;
        public string ReviewsUrl { get; set; } = default!;
        public string CometUrl { get; set; } = default!;

        public IList<string> SpywareUrls { get; set; } = default!;
        public IList<string> Unlockables { get; set; } = default!;
        public IList<ExerciseDetails> Exercises { get; set; } = default!;
    }
}
