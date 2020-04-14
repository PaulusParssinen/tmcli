using System;
using System.CommandLine;
using System.Threading.Tasks;
using System.CommandLine.Invocation;

namespace TMCli.Commands
{
    public sealed class LoginCommand : TMCCommand
    {
        public LoginCommand(CliContext context)
            : base(context, "login", "Login to TMC server")
        {
            AddArgument(new Argument<string>("username") { Description = "TMC Username" });         //TODO: Option
            AddArgument(new Argument<string>("password") { Description = "Password of the user" }); //TODO: Option
            AddArgument(new Argument<string>("organization") { Description = "MC Organization" }); //TODO: Option

            Handler = CommandHandler.Create<string, string, string>(LoginAsync);
        }

        public async Task<int> LoginAsync(string username, string password, string organization)
        {
            //TODO: Interactive prompt for the args

            if (Context.APIContext.IsAuthenticated)
            {
                Console.WriteLine($"You are already logged in!");
                Console.WriteLine("If you want to switch users, please log out first by using 'tmcli logout'-command.");
                return 1;
            }

            //TODO: Error handling

            Context.APIContext.OAuthCredentials = await TMCAPI.GetOAuthCredentialsAsync(Context.APIContext).ConfigureAwait(false);
            Context.APIContext.Token = await TMCAPI.FetchAuthorizationTokenAsync(Context.APIContext, username, password).ConfigureAwait(false);

            Context.APIContext.User = await TMCAPI.GetCurrentUserAsync(Context.APIContext).ConfigureAwait(false);

            Context.APIContext.Organization = organization;

            await Context.WriteCacheAsync().ConfigureAwait(false);
            return 0;
        }
    }
}
