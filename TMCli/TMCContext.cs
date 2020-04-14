using System;
using System.Text.Json.Serialization;

using TMCli.Json;

namespace TMCli
{
    /// <summary>
    /// Represents the current remote TMC session.
    /// </summary>
    public class TMCContext
    {
        public Uri BaseUri { get; set; } = new Uri("https://tmc.mooc.fi");

        public UserInfo? User { get; set; }
        public OAuthCredentials? OAuthCredentials { get; set; }

        [JsonIgnore]
        public bool IsAuthenticated => !string.IsNullOrEmpty(Token);
        
        public string? Token { get; set; }

        public string? Organization { get; set; }
    }
}
