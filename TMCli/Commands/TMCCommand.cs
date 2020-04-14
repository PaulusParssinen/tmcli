using System.CommandLine;

namespace TMCli.Commands
{
    public abstract class TMCCommand : Command
    {
        protected CliContext Context { get; }

        protected TMCCommand(CliContext context, string name, string? description = default)
            : base(name, description)
        {
            Context = context;
        }
    }
}
