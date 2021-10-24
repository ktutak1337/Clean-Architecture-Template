namespace CleanArchitectureTemplate.Infrastructure.Logging.Settings
{
    public class ElasticSettings
    {
        public bool Enabled { get; set; }
        public string Url { get; set; }
        public int? NumberOfShards { get; set; }
        public int? NumberOfReplicas { get; set; }
        public string IndexFormat { get; set; }
        public bool BasicAuthEnabled { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
