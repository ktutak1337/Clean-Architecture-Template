namespace CleanArchitectureTemplate.Infrastructure.Persistence.Postgres
{
    public class PostgresSettings
    {
        public string ConnectionString { get; set; }
        public bool InMemory { get; set; }
        public string InMemoryDatabaseName { get; set; }
    }
}
