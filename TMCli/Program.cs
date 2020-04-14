using System;
using System.CommandLine;
using System.Threading.Tasks;

using TMCli.Commands;

namespace TMCli
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            Console.Title = nameof(TMCli);

            var context = await CliContext.FromCacheAsync();

            var rootCommand = new RootCommand
            {
                new OrganizationCommand(context),
                new CoursesCommand(context),
                new ExercisesCommand(context),

                new DownloadCommand(context),
                new SubmitCommand(context),

                new LoginCommand(context),
                new LogoutCommand(context),
            };

            rootCommand.Description = "TestMyCode CLI";

            return await rootCommand.InvokeAsync(args);
        }
    }
}
