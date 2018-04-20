using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DeploymentTracker.web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeploymentTracker.web.ViewModels
{
    public class CreateChecklistViewModel
    {
        public List<SelectListItem> Templates { get; set; }
        [Required]
        public Guid SeletedTemplate { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ScheduleFor { get; set; }
        public string GitHash { get; set; }
        public string Comments { get; set; }
    }
}