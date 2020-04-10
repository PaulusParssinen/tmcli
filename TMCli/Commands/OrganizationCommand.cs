using System.CommandLine;

namespace TMCli.Commands
{
    public sealed class OrganizationCommand : TMCCommand
    {
        public OrganizationCommand(CliContext context)
            : base(context, "organization", "Choose organization")
        {
            AddAlias("org");

            AddArgument(new Argument<string>("organization-slug"));
        }
    }
}
