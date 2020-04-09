using System.Collections.Generic;

namespace TMCli.Json.Submission
{
    public class TestResult
    {
        public string Name { get; set; }
        public bool Succesful { get; set; }
        public string Message { get; set; }
        public string DetailedMessage { get; set; }
        public bool ValgrindFailed { get; set; }
        public IReadOnlyList<int> Points { get; set; }
        public IReadOnlyList<string> Exceptions { get; set; }
    }
}
