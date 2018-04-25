using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeploymentTracker.web.Data;
using DeploymentTracker.web.Models;
using DeploymentTracker.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DeploymentTracker.web.Controllers
{
    public class ChecklistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChecklistsController(ApplicationDbContext context) { _context = context; }

        // GET: Checklists
        public async Task<IActionResult> Index() { return View(await _context.Checklists.ToListAsync()); }

        // GET: Checklists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistEntity = await _context.Checklists
                                                .Include(x=> x.Environment)
                                                .Include(x=> x.Tasks)
                                                .ThenInclude(x=> x.Task)
                                                .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistEntity == null)
            {
                return NotFound();
            }

            return View(checklistEntity);
        }

        // GET: Checklists/Create
        public IActionResult Create()
        {
            var templates = _context.ChecklistTemplates
                                    .Where(x => x.IsActive)
                                    .Include(x => x.Environment)
                                    .Select(x => new SelectListItem { Text = $"{x.Environment.Name} - {x.Name}", Value = x.Id.ToString() }).ToList();


            var viewModel = new CreateChecklistViewModel
                            {
                                Templates = templates
            };

            return View(viewModel);
        }

        // POST: Checklists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeletedTemplate,Name,IsActive,ScheduleFor,GitHash,Comments")]
                                                CreateChecklistViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var templates = _context.ChecklistTemplates
                                        .Where(x => x.IsActive)
                                        .Include(x => x.Environment)
                                        .Select(x => new SelectListItem { Text = $"{x.Environment.Name} - {x.Name}", Value = x.Id.ToString() }).ToList();
                viewModel.Templates = templates;
                return View(viewModel);
            }


            var selectedTemplate = await _context.ChecklistTemplates
                                                 .Include(x=> x.TemplateTasks)
                                                 .ThenInclude(x=> x.Task)
                                                 .Include(x=> x.Environment)
                                   .SingleOrDefaultAsync(x => x.Id == viewModel.SeletedTemplate);

            if (selectedTemplate == null)
            {
                var templates = _context.ChecklistTemplates
                                        .Where(x => x.IsActive)
                                        .Include(x => x.Environment)
                                        .Select(x => new SelectListItem { Text = $"{x.Environment.Name} - {x.Name}", Value = x.Id.ToString() }).ToList();
                viewModel.Templates = templates;
                return View(viewModel);
            }

            var checklistEntity = new ChecklistEntity
                                  {
                                      IsActive = viewModel.IsActive,
                                      Name = viewModel.Name,
                                      Comments = viewModel.Comments,
                                      ScheduledOn = viewModel.ScheduleFor,
                                      GitHash = viewModel.GitHash,
                                      Environment = selectedTemplate.Environment
                                  };

            _context.Add(checklistEntity);
            await _context.SaveChangesAsync();

            
            var checklistTasks = selectedTemplate.TemplateTasks
                                                 .Select(x => new ChecklistTaskEntity
                                                              {
                                                                  Id = Guid.NewGuid(),
                                                                  Task = x.Task,
                                                                  SortOrder = x.SortOrder,
                                                                  Checklist = checklistEntity
                                                              })
                                                 .ToList();

            foreach (var checklistTask in checklistTasks)
            {
                _context.ChecklistTasks.Add(checklistTask);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Checklists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistEntity = await _context.Checklists
                                                .Include(x=> x.Environment)
                                                .Include(x=> x.Tasks)
                                                .ThenInclude(x=> x.Task)
                                                .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistEntity == null)
            {
                return NotFound();
            }

            return View(checklistEntity);
        }

        // POST: Checklists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ScheduledOn,StartedOn,CompletedOn,ScheduledBy,StartedBy,CompletedBy,Comments,GitHash,Name,IsActive,Id,CreatedOn,CreatedBy,LastModifiedOn,LastModifiedBy")]
                                              ChecklistEntity checklistEntity)
        {
            if (id != checklistEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checklistEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistEntityExists(checklistEntity.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(checklistEntity);
        }

        // GET: Checklists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistEntity = await _context.Checklists
                                                .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistEntity == null)
            {
                return NotFound();
            }

            return View(checklistEntity);
        }

        // POST: Checklists/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var checklistEntity = await _context.Checklists.Include(x=> x.Tasks).SingleOrDefaultAsync(m => m.Id == id);

            foreach (var task in checklistEntity.Tasks.ToList())
            {
                _context.ChecklistTasks.Remove(task);
            }


            _context.Checklists.Remove(checklistEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChecklistEntityExists(Guid id) { return _context.Checklists.Any(e => e.Id == id); }

        public async Task<IActionResult> AddCustomTask(Guid id, string name, string comments)
        {
            if (string.IsNullOrWhiteSpace(name) || id == Guid.Empty)
            {
                return BadRequest();
            }

            var checklistEntity = await _context.Checklists
                                                .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistEntity == null)
            {
                return NotFound();
            }

            var taskEntity = new TaskEntity
                             {
                                 Name = name,
                                 Comments = comments
                             };

            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();

            var checklistTask = new ChecklistTaskEntity
                                {
                                    Checklist = checklistEntity,
                                    Task = taskEntity
            };

            _context.ChecklistTasks.Add(checklistTask);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Edit), new{id});
        }

        public async Task<IActionResult> SortTasks(Guid id)
        {
            var checklistEntity = await _context.Checklists
                                                .Include(x => x.Environment)
                                                .Include(x => x.Tasks)
                                                .ThenInclude(x => x.Task)
                                                .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistEntity == null)
            {
                return NotFound();
            }

            //view
            return View(checklistEntity);
        }

        [HttpPost]
        public async Task<IActionResult> SortTasks(Guid id, List<Guid> checklistTaskIds)
        {
            var checklistEntity = await _context.Checklists
                                                .Include(x => x.Environment)
                                                .Include(x => x.Tasks)
                                                .ThenInclude(x => x.Task)
                                                .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistEntity == null)
            {
                return NotFound();
            }

            for (var i = 0; i < checklistTaskIds.Count; i++)
            {
                var checklistTask = _context.ChecklistTasks.FirstOrDefault(x => x.Id == checklistTaskIds[i]);
                if (checklistTask != null)
                {
                    checklistTask.SortOrder = i;
                    _context.Update(checklistTask);
                }
            }

            await _context.SaveChangesAsync();            
            return Ok();
        }

        public async Task<IActionResult> Start(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var checklistEntity = await _context.Checklists
                                                .Include(x=> x.Environment)
                                                .Include(x=> x.Tasks)
                                                .ThenInclude(x=> x.Task)
                                                .SingleOrDefaultAsync(m => m.Id == id && m.IsActive);
            if (checklistEntity == null)
            {
                return NotFound();
            }

            return View();
        }
    }
}