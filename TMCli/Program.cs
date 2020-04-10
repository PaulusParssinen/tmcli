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
            var context = new CliContext();

            Console.Title = "TMCli";

            var rootCommand = new RootCommand
            {
                new CoursesCommand(context),
                new ExercisesCommand(context),
                new DownloadCommand(context),
                new SubmitCommand(context),

                new LoginCommand(context),
                new LogoutCommand(context),
                new OrganizationCommand(context),
            };

            rootCommand.Description = "TestMyCode CLI";

            return await rootCommand.InvokeAsync(args);
        }
    }
}
