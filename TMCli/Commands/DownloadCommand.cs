using System;
using System.IO;
using System.CommandLine;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.CommandLine.Invocation;

namespace TMCli.Commands
{
    public sealed class DownloadCommand : TMCCommand
    {
        public DownloadCommand(CliContext context)
            : base(context, "download", "Download exercises from TMC server")
        {
            AddOption(new Option<IEnumerable<int>>("--exercise-id", "Download specified course by its unique identifier"));

            AddOption(new Option<string>("--course", "Download exercises by course name"));
            AddOption(new Option<int>("--course-id", "Download exercises by course identfier"));

            AddOption(new Option<DirectoryInfo>("--output", "If specified, the downloaded files "));

            AddOption(new Option<bool>("--all", "Download every found exercise, whether it is already completed or not"));
            AddOption(new Option<bool>("--overwrite", "If the downloaded files already exist in the output directory, it will be overwritten."));

            Handler = CommandHandler.Create<IEnumerable<int>>(HandleDownloadAsync);
        }

        public async Task<int> HandleDownloadAsync(IEnumerable<int> exerciseIds) => throw new NotImplementedException();
    }
}
