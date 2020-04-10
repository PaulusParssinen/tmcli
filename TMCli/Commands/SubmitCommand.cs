using System;
using System.IO;
using System.CommandLine;
using System.Threading.Tasks;
using System.CommandLine.Invocation;

namespace TMCli.Commands
{
    public sealed class SubmitCommand : TMCCommand
    {
        public SubmitCommand(CliContext context)
            : base(context, "submit", "Submit exercise(s)")
        {
            AddArgument(new Argument<DirectoryInfo>("directory")
            {
                Description = "Directory of the exercise"
            });

            Handler = CommandHandler.Create<DirectoryInfo>(HandleSubmitAsync);
        }

        public async Task<int> HandleSubmitAsync(DirectoryInfo directory) => throw new NotImplementedException();
    }
}
