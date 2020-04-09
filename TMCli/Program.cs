using System;
using System.CommandLine;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TMCli
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            Console.Title = "TMCli";

            //TODO: Default to mooc.fi?

            //TODO: Pull local account config & course cache?

            //TODO: Logging level

            //TODO: Aware of current dir & IO in general
            //TODO: Update exercises/status?, logout, etcetc.

            //TODO: Split to own methods/classes? + own class for displaying stuff using the System.CommnadLine rendering goodies

            var downloadCommand = new Command("download")
            {
                new Option<IEnumerable<int>>("--exercise-id")
                { 
                    Description = "Download specified course by its unique identifier"
                },
                new Option<IEnumerable<int>>("--course-id")
                {
                    //TODO: Default value will be grabbed from AccountConfig
                    Description = "Download available course exercises"
                },
                new Option<string>("--course")
                {
                    //TODO: Default value will be grabbed from AccountConfig
                    Description = "Download available course exercises"
                },
                new Option<bool>("--all")
                {
                    Description = "Download every found exercise, whether it is already completed or not" //TODO: + deadlines etc. idk gotta research
                }
            };
            var organizationCommand = new Command("organization")
            {
                new Argument<string>("organization")
                {
                    Description = ""
                }
            };
            var coursesCommand = new Command("courses")
            {
                new Argument<string>("organization")
                {
                    Description = ""
                }
            };
            var submitCommand = new Command("submit")
            {
                new Argument<string>("exercise")
                {
                    Description = "Submit exercises"
                }
            };
            var loginCommand = new Command("login")
            {
                new Option<string>("--username")
                {
                    Description = "Login"
                },
                new Option<string>("--password")
                { },
                new Option<string>("--org")
                { },
                new Option<string>("--course")
                { }
            };
            //searchCommand.Handler = CommandHandler.Create<string>(HandleDownload);

            var rootCommand = new RootCommand
            {
                organizationCommand,
                downloadCommand,
                coursesCommand,
                submitCommand,
                loginCommand,
                loginCommand,

                new Option<bool>("--version")
            };

            rootCommand.Description = "TestMyCode CLI interface.";

            return await rootCommand.InvokeAsync(args);
        }
    }
}
