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
    public class EnvironmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnvironmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Environments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Environents.ToListAsync());
        }

        // GET: Environments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentEntity = await _context.Environents
                .SingleOrDefaultAsync(m => m.Id == id);
            if (environmentEntity == null)
            {
                return NotFound();
            }

            return View(environmentEntity);
        }

        // GET: Environments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Environments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Url,Id,CreatedOn,CreatedBy,LastModifiedOn,LastModifiedBy")] EnvironmentEntity environmentEntity)
        {
            if (ModelState.IsValid)
            {
                environmentEntity.Id = Guid.NewGuid();
                _context.Add(environmentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(environmentEntity);
        }

        // GET: Environments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentEntity = await _context.Environents.SingleOrDefaultAsync(m => m.Id == id);
            if (environmentEntity == null)
            {
                return NotFound();
            }
            return View(environmentEntity);
        }

        // POST: Environments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Url,Id,CreatedOn,CreatedBy,LastModifiedOn,LastModifiedBy")] EnvironmentEntity environmentEntity)
        {
            if (id != environmentEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(environmentEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvironmentEntityExists(environmentEntity.Id))
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
            return View(environmentEntity);
        }

        // GET: Environments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentEntity = await _context.Environents
                .SingleOrDefaultAsync(m => m.Id == id);
            if (environmentEntity == null)
            {
                return NotFound();
            }

            return View(environmentEntity);
        }

        // POST: Environments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var environmentEntity = await _context.Environents.SingleOrDefaultAsync(m => m.Id == id);
            _context.Environents.Remove(environmentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvironmentEntityExists(Guid id)
        {
            return _context.Environents.Any(e => e.Id == id);
        }
    }
}
