using System.Collections.Generic;

namespace TMCli.Json.Submission
{
    public class TestResult
    {
        public string Name { get; set; } = default!;
        public bool Succesful { get; set; }
        public string Message { get; set; } = default!;
        public string DetailedMessage { get; set; } = default!;
        public bool ValgrindFailed { get; set; }
        public IReadOnlyList<int> Points { get; set; } = default!;
        public IReadOnlyList<string> Exceptions { get; set; } = default!;
    }
}
