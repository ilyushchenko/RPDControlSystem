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
    public class ProfileCompetencesController : Controller
    {
        private readonly DatabaseContext _context;

        public ProfileCompetencesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ProfileCompetences
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.ProfileCompetence.Include(p => p.Competence).Include(p => p.Profile);
            return View(await databaseContext.ToListAsync());
        }

        // GET: ProfileCompetences/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileCompetence = await _context.ProfileCompetence
                .Include(p => p.Competence)
                .Include(p => p.Profile)
                .SingleOrDefaultAsync(m => m.ProfileCode == id);
            if (profileCompetence == null)
            {
                return NotFound();
            }

            return View(profileCompetence);
        }

        // GET: ProfileCompetences/Create
        public IActionResult Create()
        {
            ViewData["CompetenceId"] = new SelectList(_context.Competence, "Id", "Code");
            ViewData["ProfileCode"] = new SelectList(_context.Profile, "Code", "Code");
            return View();
        }

        // POST: ProfileCompetences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileCode,CompetenceId")] ProfileCompetence profileCompetence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profileCompetence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetenceId"] = new SelectList(_context.Competence, "Id", "Code", profileCompetence.CompetenceId);
            ViewData["ProfileCode"] = new SelectList(_context.Profile, "Code", "Code", profileCompetence.ProfileCode);
            return View(profileCompetence);
        }

        // GET: ProfileCompetences/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileCompetence = await _context.ProfileCompetence.SingleOrDefaultAsync(m => m.ProfileCode == id);
            if (profileCompetence == null)
            {
                return NotFound();
            }
            ViewData["CompetenceId"] = new SelectList(_context.Competence, "Id", "Code", profileCompetence.CompetenceId);
            ViewData["ProfileCode"] = new SelectList(_context.Profile, "Code", "Code", profileCompetence.ProfileCode);
            return View(profileCompetence);
        }

        // POST: ProfileCompetences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProfileCode,CompetenceId")] ProfileCompetence profileCompetence)
        {
            if (id != profileCompetence.ProfileCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profileCompetence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileCompetenceExists(profileCompetence.ProfileCode))
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
            ViewData["CompetenceId"] = new SelectList(_context.Competence, "Id", "Code", profileCompetence.CompetenceId);
            ViewData["ProfileCode"] = new SelectList(_context.Profile, "Code", "Code", profileCompetence.ProfileCode);
            return View(profileCompetence);
        }

        // GET: ProfileCompetences/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileCompetence = await _context.ProfileCompetence
                .Include(p => p.Competence)
                .Include(p => p.Profile)
                .SingleOrDefaultAsync(m => m.ProfileCode == id);
            if (profileCompetence == null)
            {
                return NotFound();
            }

            return View(profileCompetence);
        }

        // POST: ProfileCompetences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var profileCompetence = await _context.ProfileCompetence.SingleOrDefaultAsync(m => m.ProfileCode == id);
            _context.ProfileCompetence.Remove(profileCompetence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileCompetenceExists(string id)
        {
            return _context.ProfileCompetence.Any(e => e.ProfileCode == id);
        }
    }
}
