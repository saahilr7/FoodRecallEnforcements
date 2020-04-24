using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodRecallEnforcements.DataAccess;
using FoodRecallEnforcements.Models;

namespace FoodRecallEnforcements.Controllers
{
    public class RecallController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecallController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recall
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recalls.Include(r => r.classification).Include(r => r.location);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recall/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recall = await _context.Recalls
                .Include(r => r.classification)
                .Include(r => r.location)
                .FirstOrDefaultAsync(m => m.RecallId == id);
            if (recall == null)
            {
                return NotFound();
            }

            return View(recall);
        }

        // GET: Recall/Create
        public IActionResult AddOrEdit(int id=0)
        {
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "classificationId", "classificationId");
            ViewData["LocationId"] = new SelectList(_context.Locations, "AddressId", "AddressId");
            return View(new Recall());
        }

        // POST: Recall/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecallId,reason_for_recall,code_info,product_quantity,distribution_pattern,product_description,report_date,recall_number,recalling_firm,initial_firm_notification,event_id,product_type,termination_date,recall_initiation_date,voluntary_mandated,LocationId,ClassificationId")] Recall recall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "classificationId", "classificationId", recall.ClassificationId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "AddressId", "AddressId", recall.LocationId);
            return View(recall);
        }

        // GET: Recall/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recall = await _context.Recalls.FindAsync(id);
            if (recall == null)
            {
                return NotFound();
            }
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "classificationId", "classificationId", recall.ClassificationId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "AddressId", "AddressId", recall.LocationId);
            return View(recall);
        }

        // POST: Recall/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecallId,reason_for_recall,code_info,product_quantity,distribution_pattern,product_description,report_date,recall_number,recalling_firm,initial_firm_notification,event_id,product_type,termination_date,recall_initiation_date,voluntary_mandated,LocationId,ClassificationId")] Recall recall)
        {
            if (id != recall.RecallId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecallExists(recall.RecallId))
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
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "classificationId", "classificationId", recall.ClassificationId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "AddressId", "AddressId", recall.LocationId);
            return View(recall);
        }

        // GET: Recall/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recall = await _context.Recalls
                .Include(r => r.classification)
                .Include(r => r.location)
                .FirstOrDefaultAsync(m => m.RecallId == id);
            if (recall == null)
            {
                return NotFound();
            }

            return View(recall);
        }

        // POST: Recall/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recall = await _context.Recalls.FindAsync(id);
            _context.Recalls.Remove(recall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecallExists(int id)
        {
            return _context.Recalls.Any(e => e.RecallId == id);
        }
    }
}
