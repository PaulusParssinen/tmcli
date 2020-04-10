using System.CommandLine;

namespace TMCli.Commands
{
    public sealed class CoursesCommand : TMCCommand
    {
        public CoursesCommand(CliContext context) 
            : base(context, "courses", "Show the available courses")
        {
            AddArgument(new Argument<string>("organization"));
        }
    }
}
