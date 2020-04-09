using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TMCli.Json
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonPropertyName("organization_slug")]
        public string OrganizationSlug { get; set; }

        public IList<Exercise> Exercises { get; set; }
        
        public string DetailsUrl { get; set; }
        public string UnlockUrl { get; set; }
        public string CometUrl { get; set; }

        public IList<string> SpywareUrls { get; set; }
        public string ReviewsUrl { get; set; }
        public IList<string> Unlockables { get; set; }
    }
}
