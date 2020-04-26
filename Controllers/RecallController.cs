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
            var applicationDbContext = _context.Recalls.Include(r => r.classification).Include(r => r.location).Take(10);
            return View(await applicationDbContext.ToListAsync());

            //List<Enforcement> enforcement = dbContext.Enforcements.ToList();

            //var results = enforcement.Take(10);
            //return View(results);
        }


        //SEARCH BY FIRM
        public IActionResult SearchFirm(string searching)
        {
            return View(_context.Recalls.Where(x => x.recalling_firm.Contains(searching) || searching == null).ToList().Take(10));
        }

        //Search By Voluntary or Mandated
        public ActionResult SearchBy(string searchBy, string search)
        {
            if (searchBy == "Voluntary or Mandated")
                return View(_context.Recalls.Where(x => x.voluntary_mandated.Contains(search) || search == null).ToList().Take(10));
            else
                return View(_context.Recalls.Where(x => x.recalling_firm.Contains(search) || search == null).ToList().Take(10));
        }
 

        // GET: Recall/AddOrEdit
        public IActionResult AddOrEdit(int id=0)
        {
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "classificationId", "classificationId");
            ViewData["LocationId"] = new SelectList(_context.Locations, "AddressId", "AddressId");

            if (id == 0)
                return View(new Recall());
            else
                return View(_context.Recalls.Find(id));
        }

        // POST: Recall/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("RecallId,reason_for_recall,code_info,product_quantity,distribution_pattern,product_description,report_date,recall_number,recalling_firm,initial_firm_notification,event_id,product_type,termination_date,recall_initiation_date,voluntary_mandated,LocationId,ClassificationId")] Recall recall)
        {
            if (ModelState.IsValid)
            {
                if (recall.RecallId == 0)
                    _context.Add(recall);
                else
                    _context.Update(recall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassificationId"] = new SelectList(_context.Classifications, "classificationId", "classificationId", recall.ClassificationId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "AddressId", "AddressId", recall.LocationId);
            return View(recall);
        }

   
        // GET: Recall/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
