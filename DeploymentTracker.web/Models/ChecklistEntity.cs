using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeploymentTracker.web.Models
{
    public class ChecklistEntity : BaseEntity
    {
        public EnvironmentEntity Environment { get; set; }
        public ICollection<ChecklistTaskEntity> Tasks { get; set; }
        public DateTime? ScheduledOn { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public string ScheduledBy { get; set; }
        public string StartedBy { get; set; }
        public string CompletedBy { get; set; }
        public string Comments { get; set; }
        public string GitHash { get; set; }
    }

    public class ChecklistTaskEntity : BaseEntity
    {
        [Required]
        public TaskEntity Task { get; set; }
        [Required]
        public ChecklistEntity Checklist { get; set; }
    }
}