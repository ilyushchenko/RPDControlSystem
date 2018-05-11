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
    public class DisciplineCompetencesController : Controller
    {
        private readonly DatabaseContext _context;

        public DisciplineCompetencesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: DisciplineCompetences
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.DisciplineCompetence.Include(d => d.Competence).Include(d => d.DisciplineInfo);
            return View(await databaseContext.ToListAsync());
        }

        // GET: DisciplineCompetences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineCompetence = await _context.DisciplineCompetence
                .Include(d => d.Competence)
                .Include(d => d.DisciplineInfo)
                .SingleOrDefaultAsync(m => m.DisciplineInfoId == id);
            if (disciplineCompetence == null)
            {
                return NotFound();
            }

            return View(disciplineCompetence);
        }

        // GET: DisciplineCompetences/Create
        public IActionResult Create()
        {
            ViewData["CompetenceId"] = new SelectList(_context.Competence, "Id", "Code");
            ViewData["DisciplineInfoId"] = new SelectList(_context.DisciplineInfo, "Id", "DisciplineCode");
            return View();
        }

        // POST: DisciplineCompetences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DisciplineInfoId,CompetenceId")] DisciplineCompetence disciplineCompetence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplineCompetence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetenceId"] = new SelectList(_context.Competence, "Id", "Code", disciplineCompetence.CompetenceId);
            ViewData["DisciplineInfoId"] = new SelectList(_context.DisciplineInfo, "Id", "DisciplineCode", disciplineCompetence.DisciplineInfoId);
            return View(disciplineCompetence);
        }

        // GET: DisciplineCompetences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineCompetence = await _context.DisciplineCompetence.SingleOrDefaultAsync(m => m.DisciplineInfoId == id);
            if (disciplineCompetence == null)
            {
                return NotFound();
            }
            ViewData["CompetenceId"] = new SelectList(_context.Competence, "Id", "Code", disciplineCompetence.CompetenceId);
            ViewData["DisciplineInfoId"] = new SelectList(_context.DisciplineInfo, "Id", "DisciplineCode", disciplineCompetence.DisciplineInfoId);
            return View(disciplineCompetence);
        }

        // POST: DisciplineCompetences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DisciplineInfoId,CompetenceId")] DisciplineCompetence disciplineCompetence)
        {
            if (id != disciplineCompetence.DisciplineInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disciplineCompetence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplineCompetenceExists(disciplineCompetence.DisciplineInfoId))
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
            ViewData["CompetenceId"] = new SelectList(_context.Competence, "Id", "Code", disciplineCompetence.CompetenceId);
            ViewData["DisciplineInfoId"] = new SelectList(_context.DisciplineInfo, "Id", "DisciplineCode", disciplineCompetence.DisciplineInfoId);
            return View(disciplineCompetence);
        }

        // GET: DisciplineCompetences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineCompetence = await _context.DisciplineCompetence
                .Include(d => d.Competence)
                .Include(d => d.DisciplineInfo)
                .SingleOrDefaultAsync(m => m.DisciplineInfoId == id);
            if (disciplineCompetence == null)
            {
                return NotFound();
            }

            return View(disciplineCompetence);
        }

        // POST: DisciplineCompetences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplineCompetence = await _context.DisciplineCompetence.SingleOrDefaultAsync(m => m.DisciplineInfoId == id);
            _context.DisciplineCompetence.Remove(disciplineCompetence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisciplineCompetenceExists(int id)
        {
            return _context.DisciplineCompetence.Any(e => e.DisciplineInfoId == id);
        }
    }
}
