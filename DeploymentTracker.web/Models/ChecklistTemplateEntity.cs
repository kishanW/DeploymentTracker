using System.Collections.Generic;

namespace DeploymentTracker.web.Models
{
    public class ChecklistTemplateEntity: BaseEntity
    {
        public EnvironmentEntity Environment { get; set; }
        public List<ChecklistTemplateTaskEntity> TemplateTasks { get; set; }
    }


    public class ChecklistTemplateTaskEntity : BaseEntity
    {
        public ChecklistTemplateEntity Template { get; set; }
        public TaskEntity Task { get; set; }
    }
}