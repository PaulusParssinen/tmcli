using System.IO;

namespace TMCli
{
    public class CliContext
    {
        public DirectoryInfo CurrentDirectory { get; set; } = new DirectoryInfo(Directory.GetCurrentDirectory());
   
        //TODO: Caches & access to current TMCContext?
    }
}
