using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPDControlSystem.Models.RPD;
using RPDControlSystem.Storage;

namespace RPDControlSystem.Controllers
{
    public class PlansController : Controller
    {
        private readonly DatabaseContext _context;

        public PlansController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Plans
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Plan.Include(p => p.Profile);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Plans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .Include(p => p.Profile)
                .Include(d => d.Disciplines)
                    .ThenInclude(c => c.Discipline)
                .SingleOrDefaultAsync(m => m.Code == id);
            if (plan == null)
            {
                return NotFound();
            }

            var disciplinesToAdd = _context.Discipline;

            var result = disciplinesToAdd.Except(plan.Disciplines.Select( d => d.Discipline));


            ViewData["DisciplineCode"] = new SelectList(result, "Code", "Name");

            return View(plan);
        }

        // GET: Plans/Create
        public IActionResult Create()
        {
            ViewData["ProfileCode"] = new SelectList(_context.Profile, "Code", "Code");
            return View();
        }

        // POST: Plans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,EducationForm,ProfileCode")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfileCode"] = new SelectList(_context.Profile, "Code", "Code", plan.ProfileCode);
            return View(plan);
        }

        // GET: Plans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan.SingleOrDefaultAsync(m => m.Code == id);
            if (plan == null)
            {
                return NotFound();
            }
            ViewData["ProfileCode"] = new SelectList(_context.Profile, "Code", "Code", plan.ProfileCode);
            return View(plan);
        }

        // POST: Plans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,EducationForm,ProfileCode")] Plan plan)
        {
            if (id != plan.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(plan.Code))
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
            ViewData["ProfileCode"] = new SelectList(_context.Profile, "Code", "Code", plan.ProfileCode);
            return View(plan);
        }

        // GET: Plans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .Include(p => p.Profile)
                .SingleOrDefaultAsync(m => m.Code == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var plan = await _context.Plan.SingleOrDefaultAsync(m => m.Code == id);
            _context.Plan.Remove(plan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanExists(string id)
        {
            return _context.Plan.Any(e => e.Code == id);
        }

        // POST: DisciplineInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDisciplineInfo([Bind("Id,DisciplineCode,PlanCode,DisciplineType")] DisciplineInfo disciplineInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplineInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = disciplineInfo.PlanCode });
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
