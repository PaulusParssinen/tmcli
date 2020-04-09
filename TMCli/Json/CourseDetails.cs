using System.Collections.Generic;

namespace TMCli.Json
{
    public class CourseDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string UnlockUrl { get; set; }
        public string DetailsUrl { get; set; }
        public string ReviewsUrl { get; set; }
        public string CometUrl { get; set; }

        public IList<string> SpywareUrls { get; set; }
        public IList<string> Unlockables { get; set; }
        public IList<ExerciseDetails> Exercises { get; set; }
    }
}
