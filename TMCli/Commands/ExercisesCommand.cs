using System;
using System.CommandLine;
using System.Threading.Tasks;
using System.CommandLine.Invocation;

namespace TMCli.Commands
{
    public sealed class ExercisesCommand : TMCCommand
    {
        public ExercisesCommand(CliContext context)
            : base(context, "exercises", "List and update exercises for a specific course")
        {
            //TODO: If no CurrentCourse nor specified course to fetch exercises => info msg

            AddOption(new Option<string>("--course", "Name of the course"));

            var updateCommand = new Command("update", "Update exercises")
            {
                Handler = CommandHandler.Create(UpdateExercisesAsync)
            };
            AddCommand(updateCommand);
        }

        private async Task<int> UpdateExercisesAsync() => throw new NotImplementedException();
    }
}
