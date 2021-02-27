namespace CleanArchitectureTemplate.Infrastructure.Swagger
{
    public class SwaggerSettings
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string RoutePrefix { get; set; }
        public bool Authorization { get; set; }
        public OAuth2 OAuth2 { get; set; }
        public bool CommentsEnabled { get; set; }
    }
}
