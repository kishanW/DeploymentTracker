using System;
using Microsoft.AspNetCore.Mvc;

namespace DeploymentTracker.web.Controllers
{
    public class ChecklistController: Controller
    {
        public IActionResult Index(Guid? id)
        {
            //do work
            return View();
        }

        public IActionResult Create()
        {
            //create checklist
            return null;
        }
        public IActionResult AddTask()
        {
            //add checklist task
            return null;
        }

        public IActionResult AddEnvironment()
        {
            //add environment
            return null;
        }

        public IActionResult Task()
        {
            //add task
            return null;
        }

        public IActionResult AddTemplate()
        {
            //add checklist template
            return null;
        }

        public IActionResult AddTemplateTask()
        {
            //add checklist template task
            return null;
        }
    }
}