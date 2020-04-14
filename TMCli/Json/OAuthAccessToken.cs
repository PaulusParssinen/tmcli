namespace TMCli.Json
{
    public class OAuthAccessToken
    {
        public string AccessToken { get; set; } = default!;
        public string TokentType { get; set; } = default!;
        public string Scope { get; set; } = default!;
        public int CreatedAt { get; set; }
    }
}
