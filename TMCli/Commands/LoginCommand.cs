namespace TMCli.Commands
{
    public sealed class LoginCommand : TMCCommand
    {
        public LoginCommand(CliContext context) 
            : base(context, "login", "Login to TMC server")
        { }
    }
}
