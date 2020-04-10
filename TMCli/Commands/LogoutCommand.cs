namespace TMCli.Commands
{
    public sealed class LogoutCommand : TMCCommand
    {
        public LogoutCommand(CliContext context)
            : base(context, "logout", "Logout from TMC server")
        { }
    }
}
