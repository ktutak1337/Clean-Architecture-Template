using System.Collections.Generic;

namespace CleanArchitectureTemplate.Infrastructure.Logging.Settings
{
    public class LoggerSettings
    {
        public string Level { get; set; }
        public ConsoleSettings Console { get; set; }
        public FileSettings File { get; set; }
        #if (serilogSeq)
        public SeqSettings Seq { get; set; }
        #endif
        public IDictionary<string, string> Overrides { get; set; }
        public IEnumerable<string> ExcludePaths { get; set; }
        public IEnumerable<string> ExcludeProperties { get; set; }
        public IDictionary<string, object> Tags { get; set; }
    }
}
