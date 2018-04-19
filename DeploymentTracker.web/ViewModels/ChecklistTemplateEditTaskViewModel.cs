using System;
using System.Collections.Generic;
using DeploymentTracker.web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeploymentTracker.web.ViewModels
{
    public class ChecklistTemplateEditTaskViewModel
    {
        public ChecklistTemplateEntity ChecklistTemplate { get; set; }

        public Guid? SelectedEnvironment { get; set; }
        public List<SelectListItem> Environments { get; set; }

        public List<Guid> SelectedTasks { get; set; }
        public List<SelectListItem> Tasks { get; set; }
        public Guid Id { get; set; }
    }
}