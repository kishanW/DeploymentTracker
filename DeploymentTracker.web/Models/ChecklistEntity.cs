﻿using System;
using System.Collections.Generic;

namespace DeploymentTracker.web.Models
{
    public class ChecklistEntity : BaseEntity
    {
        public EnvironmentEntity Environment { get; set; }
        public List<ChecklistTaskEntity> Tasks { get; set; }
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
        public TaskEntity Task { get; set; }
        public ChecklistEntity Checklist { get; set; }
    }
}