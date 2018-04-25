using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeploymentTracker.web.Models
{
    public class ChecklistTemplateEntity: BaseEntity
    {
        public EnvironmentEntity Environment { get; set; }
        public ICollection<ChecklistTemplateTaskEntity> TemplateTasks { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }


    public class ChecklistTemplateTaskEntity : BaseEntity
    {
        [Required]
        public ChecklistTemplateEntity Template { get; set; }
        [Required]
        public TaskEntity Task { get; set; }
        public int SortOrder { get; set; }
    }
}