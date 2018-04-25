using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeploymentTracker.web.Data;
using DeploymentTracker.web.Models;
using DeploymentTracker.web.ViewModels;

namespace DeploymentTracker.web.Controllers
{
    public class ChecklistTemplatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChecklistTemplatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChecklistTemplates
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChecklistTemplates.ToListAsync());
        }

        // GET: ChecklistTemplates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistTemplateEntity = await _context.ChecklistTemplates
                .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistTemplateEntity == null)
            {
                return NotFound();
            }

            return View(checklistTemplateEntity);
        }

        // GET: ChecklistTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChecklistTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsActive,Id,CreatedOn,CreatedBy,LastModifiedOn,LastModifiedBy")] ChecklistTemplateEntity checklistTemplateEntity)
        {
            if (ModelState.IsValid)
            {
                checklistTemplateEntity.Id = Guid.NewGuid();
                _context.Add(checklistTemplateEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checklistTemplateEntity);
        }

        // GET: ChecklistTemplates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistTemplateEntity = await _context.ChecklistTemplates
                                                        .Include(x=> x.TemplateTasks)
                                                        .ThenInclude(x=> x.Task)
                                                        .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistTemplateEntity == null)
            {
                return NotFound();
            }
            return View(checklistTemplateEntity);
        }

        // POST: ChecklistTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,IsActive,Id,CreatedOn,CreatedBy,LastModifiedOn,LastModifiedBy")] ChecklistTemplateEntity checklistTemplateEntity)
        {
            if (id != checklistTemplateEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checklistTemplateEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistTemplateEntityExists(checklistTemplateEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(checklistTemplateEntity);
        }

        // GET: ChecklistTemplates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistTemplateEntity = await _context.ChecklistTemplates
                .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistTemplateEntity == null)
            {
                return NotFound();
            }

            return View(checklistTemplateEntity);
        }

        // POST: ChecklistTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var checklistTemplateEntity = await _context.ChecklistTemplates.SingleOrDefaultAsync(m => m.Id == id);
            _context.ChecklistTemplates.Remove(checklistTemplateEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChecklistTemplateEntityExists(Guid id)
        {
            return _context.ChecklistTemplates.Any(e => e.Id == id);
        }


        public async Task<IActionResult> EditTemplate(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistTemplateEntity = await _context.ChecklistTemplates
                                                        .Include(x=> x.TemplateTasks)
                                                        .Include(x=> x.Environment)
                                                        .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistTemplateEntity == null)
            {
                return NotFound();
            }

            var environments = await _context.Environents.ToListAsync();
            var tasks = await _context.Tasks.ToListAsync();

            var viewModel = new ChecklistTemplateEditTaskViewModel
                            {
                                Id = checklistTemplateEntity.Id,
                                ChecklistTemplate = checklistTemplateEntity,
                                Environments = environments.Select(x=> new SelectListItem {Text = x.Name, Value = x.Id.ToString(), Selected = checklistTemplateEntity.Environment?.Id != null && checklistTemplateEntity.Environment?.Id == x.Id }).ToList(),
                                Tasks = tasks.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = checklistTemplateEntity.TemplateTasks.Any(t=> t.Task.Id == x.Id)}).ToList()
                            };
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTemplate(Guid id, [Bind("Id,SelectedEnvironment,SelectedTasks")] ChecklistTemplateEditTaskViewModel viewModel)
        {
            if (viewModel.Id == Guid.Empty)
            {
                return NotFound();
            }

            var checklistTemplateEntity = await _context.ChecklistTemplates
                                                        .SingleOrDefaultAsync(m => m.Id == viewModel.Id);
            if (checklistTemplateEntity == null)
            {
                return NotFound();
            }


            var selectedTasks = _context.Tasks.Where(m => viewModel.SelectedTasks.Contains(m.Id));
            var selectedEnvironment = await _context.Environents.SingleOrDefaultAsync(m => m.Id == viewModel.SelectedEnvironment);

            try
            {
                checklistTemplateEntity.Environment = selectedEnvironment;
                var existingTemplateTasks = _context.ChecklistTemplateTasks
                                                    .Where(ctt => ctt.Template.Id == checklistTemplateEntity.Id);

                //add new template tasks
                foreach (var selectedTask in selectedTasks)
                {
                    var isExistingTemplateTask = existingTemplateTasks.Any(ctt => ctt.Task.Id == selectedTask.Id);
                    if (!isExistingTemplateTask)
                    {
                        var newTemplateTask = new ChecklistTemplateTaskEntity
                                              {
                                                  Task = selectedTask,
                                                  Template = checklistTemplateEntity
                                              };
                        _context.ChecklistTemplateTasks.Add(newTemplateTask);
                    }
                }

                //remove old ones
                var templateTasksToRemove = existingTemplateTasks.Where(x => !selectedTasks.Any(st => st.Id == x.Task.Id));
                foreach (var existingTemplateTask in templateTasksToRemove)
                {
                    if (selectedTasks.All(x=> x.Id != existingTemplateTask.Id))
                    {
                        _context.ChecklistTemplateTasks.Remove(existingTemplateTask);
                    }
                }

                _context.Update(checklistTemplateEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChecklistTemplateEntityExists(checklistTemplateEntity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SortTasks(Guid id)
        {
            var checklistTemplateEntity = await _context.ChecklistTemplates
                                                        .Include(x=> x.TemplateTasks)
                                                        .ThenInclude(x=> x.Task)
                                                        .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistTemplateEntity == null)
            {
                return NotFound();
            }
            return View(checklistTemplateEntity);
        }


        [HttpPost]
        public async Task<IActionResult> SortTasks(Guid id, List<Guid> checklistTemplateTaskIds)
        {
            for (var i = 0; i < checklistTemplateTaskIds.Count; i++)
            {
                var checklistTemplateTask = _context.ChecklistTemplateTasks.FirstOrDefault(x => x.Id == checklistTemplateTaskIds[i]);
                if (checklistTemplateTask != null)
                {
                    checklistTemplateTask.SortOrder = i;
                    _context.Update(checklistTemplateTask);
                }
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
