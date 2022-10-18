using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLabsAsp.Data;
using WebLabsAsp.Entities;

namespace WebLabsAsp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarGroupController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarGroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarGroup
        public async Task<IActionResult> Index()
        {
              return _context.CarGroups != null ? 
                          View(await _context.CarGroups.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CarGroups'  is null.");
        }
        
        // GET: CarGroup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarGroup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarGroupId,GroupName")] CarGroup carGroup)
        {
            if (ModelState.IsValid)
            {
                carGroup.CarGroupId = Guid.NewGuid();
                _context.Add(carGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carGroup);
        }

        // GET: CarGroup/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.CarGroups == null)
            {
                return NotFound();
            }

            var carGroup = await _context.CarGroups.FindAsync(id);
            if (carGroup == null)
            {
                return NotFound();
            }
            return View(carGroup);
        }

        // POST: CarGroup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CarGroupId,GroupName")] CarGroup carGroup)
        {
            if (id != carGroup.CarGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarGroupExists(carGroup.CarGroupId))
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
            return View(carGroup);
        }

        // GET: CarGroup/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CarGroups == null)
            {
                return NotFound();
            }

            var carGroup = await _context.CarGroups
                .FirstOrDefaultAsync(m => m.CarGroupId == id);
            if (carGroup == null)
            {
                return NotFound();
            }

            return View(carGroup);
        }

        // POST: CarGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.CarGroups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarGroups'  is null.");
            }
            var carGroup = await _context.CarGroups.FindAsync(id);
            if (carGroup != null)
            {
                _context.CarGroups.Remove(carGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarGroupExists(Guid id)
        {
          return (_context.CarGroups?.Any(e => e.CarGroupId == id)).GetValueOrDefault();
        }
    }
}
