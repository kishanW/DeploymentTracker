using System;
using System.Linq;
using System.Threading.Tasks;
using DeploymentTracker.web.Data;
using DeploymentTracker.web.Models;
using Microsoft.AspNetCore.Mvc;
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
                                                .SingleOrDefaultAsync(m => m.Id == id);
            if (checklistEntity == null)
            {
                return NotFound();
            }

            return View(checklistEntity);
        }

        // GET: Checklists/Create
        public IActionResult Create() { return View(); }

        // POST: Checklists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduledOn,StartedOn,CompletedOn,ScheduledBy,StartedBy,CompletedBy,Comments,GitHash,Name,IsActive,Id,CreatedOn,CreatedBy,LastModifiedOn,LastModifiedBy")]
                                                ChecklistEntity checklistEntity)
        {
            if (ModelState.IsValid)
            {
                checklistEntity.Id = Guid.NewGuid();
                _context.Add(checklistEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(checklistEntity);
        }

        // GET: Checklists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistEntity = await _context.Checklists.SingleOrDefaultAsync(m => m.Id == id);
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
            var checklistEntity = await _context.Checklists.SingleOrDefaultAsync(m => m.Id == id);
            _context.Checklists.Remove(checklistEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChecklistEntityExists(Guid id) { return _context.Checklists.Any(e => e.Id == id); }
    }
}