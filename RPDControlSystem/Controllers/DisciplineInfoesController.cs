using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPDControlSystem.Models;
using RPDControlSystem.Models.RPD;
using RPDControlSystem.Storage;

namespace RPDControlSystem.Controllers
{
    public class DisciplineInfoesController : Controller
    {
        private readonly DatabaseContext _context;

        public DisciplineInfoesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: DisciplineInfoes
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.DisciplineInfo.Include(d => d.Discipline).Include(d => d.Plan).Include(d => d.WorkPlan);
            return View(await databaseContext.ToListAsync());
        }

        // GET: DisciplineInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineInfo = await _context.DisciplineInfo
                .Include(d => d.Discipline)
                .Include(d => d.Plan)
                .Include(d => d.WorkPlan)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (disciplineInfo == null)
            {
                return NotFound();
            }

            return View(disciplineInfo);
        }

        // GET: DisciplineInfoes/Create
        public IActionResult Create()
        {
            ViewData["DisciplineCode"] = new SelectList(_context.Discipline, "Code", "Code");
            ViewData["PlanCode"] = new SelectList(_context.Plan, "Code", "Code");
            ViewData["WorkPlanId"] = new SelectList(_context.Set<File>(), "Id", "BaseName");
            return View();
        }

        // POST: DisciplineInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DisciplineCode,PlanCode,DisciplineType,WorkPlanId")] DisciplineInfo disciplineInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplineInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisciplineCode"] = new SelectList(_context.Discipline, "Code", "Code", disciplineInfo.DisciplineCode);
            ViewData["PlanCode"] = new SelectList(_context.Plan, "Code", "Code", disciplineInfo.PlanCode);
            ViewData["WorkPlanId"] = new SelectList(_context.Set<File>(), "Id", "BaseName", disciplineInfo.WorkPlanId);
            return View(disciplineInfo);
        }

        // GET: DisciplineInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineInfo = await _context.DisciplineInfo.SingleOrDefaultAsync(m => m.Id == id);
            if (disciplineInfo == null)
            {
                return NotFound();
            }
            ViewData["DisciplineCode"] = new SelectList(_context.Discipline, "Code", "Code", disciplineInfo.DisciplineCode);
            ViewData["PlanCode"] = new SelectList(_context.Plan, "Code", "Code", disciplineInfo.PlanCode);
            ViewData["WorkPlanId"] = new SelectList(_context.Set<File>(), "Id", "BaseName", disciplineInfo.WorkPlanId);
            return View(disciplineInfo);
        }

        // POST: DisciplineInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DisciplineCode,PlanCode,DisciplineType,WorkPlanId")] DisciplineInfo disciplineInfo)
        {
            if (id != disciplineInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplineInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplineInfoExists(disciplineInfo.Id))
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
            ViewData["DisciplineCode"] = new SelectList(_context.Discipline, "Code", "Code", disciplineInfo.DisciplineCode);
            ViewData["PlanCode"] = new SelectList(_context.Plan, "Code", "Code", disciplineInfo.PlanCode);
            ViewData["WorkPlanId"] = new SelectList(_context.Set<File>(), "Id", "BaseName", disciplineInfo.WorkPlanId);
            return View(disciplineInfo);
        }

        // GET: DisciplineInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineInfo = await _context.DisciplineInfo
                .Include(d => d.Discipline)
                .Include(d => d.Plan)
                .Include(d => d.WorkPlan)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (disciplineInfo == null)
            {
                return NotFound();
            }

            return View(disciplineInfo);
        }

        // POST: DisciplineInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplineInfo = await _context.DisciplineInfo.SingleOrDefaultAsync(m => m.Id == id);
            _context.DisciplineInfo.Remove(disciplineInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplineInfoExists(int id)
        {
            return _context.DisciplineInfo.Any(e => e.Id == id);
        }
    }
}
