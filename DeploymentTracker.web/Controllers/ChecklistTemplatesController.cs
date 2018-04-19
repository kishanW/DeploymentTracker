using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeploymentTracker.web.Data;
using DeploymentTracker.web.Models;

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

            var checklistTemplateEntity = await _context.ChecklistTemplates.SingleOrDefaultAsync(m => m.Id == id);
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
    }
}
