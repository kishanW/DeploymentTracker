namespace DeploymentTracker.web.Models
{
    public class EnvironmentEntity: BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}