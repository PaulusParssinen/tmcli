namespace TMCli.Json
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool Administrator { get; set; }
    }
}
