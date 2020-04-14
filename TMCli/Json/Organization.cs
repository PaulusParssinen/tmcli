namespace TMCli.Json
{
    public class Organization
    {
        public string Name { get; set; } = default!;
        public string Information { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string LogoPath { get; set; } = default!;
        public bool Pinned { get; set; }
    }
}
