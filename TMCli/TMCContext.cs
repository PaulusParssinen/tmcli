using System;

using TMCli.Json;

namespace TMCli
{
    public class TMCContext
    {
        public Uri BaseUri { get; set; } = new Uri("https://tmc.mooc.fi");

        public OAuthCredentials? OAuthCredentials { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
