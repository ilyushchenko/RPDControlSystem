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
    public class TeacherProfilesController : Controller
    {
        private readonly DatabaseContext _context;

        public TeacherProfilesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: TeacherProfiles
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.TeacherProfiles.Include(t => t.Degree).Include(t => t.Photo).Include(t => t.Post);
            return View(await databaseContext.ToListAsync());
        }

        // GET: TeacherProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherProfile = await _context.TeacherProfiles
                .Include(t => t.Degree)
                .Include(t => t.Photo)
                .Include(t => t.Post)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (teacherProfile == null)
            {
                return NotFound();
            }

            return View(teacherProfile);
        }

        // GET: TeacherProfiles/Create
        public IActionResult Create()
        {
            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name");
            return View();
        }

        // POST: TeacherProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,DegreeId,PostId,PhotoId")] TeacherProfile teacherProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name", teacherProfile.DegreeId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", teacherProfile.PostId);
            return View(teacherProfile);
        }

        // GET: TeacherProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherProfile = await _context.TeacherProfiles.SingleOrDefaultAsync(m => m.Id == id);
            if (teacherProfile == null)
            {
                return NotFound();
            }
            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name", teacherProfile.DegreeId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", teacherProfile.PostId);
            return View(teacherProfile);
        }

        // POST: TeacherProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,DegreeId,PostId,PhotoId")] TeacherProfile teacherProfile)
        {
            if (id != teacherProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherProfileExists(teacherProfile.Id))
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
            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name", teacherProfile.DegreeId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", teacherProfile.PostId);
            return View(teacherProfile);
        }

        // GET: TeacherProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherProfile = await _context.TeacherProfiles
                .Include(t => t.Degree)
                .Include(t => t.Photo)
                .Include(t => t.Post)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (teacherProfile == null)
            {
                return NotFound();
            }

            return View(teacherProfile);
        }

        // POST: TeacherProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherProfile = await _context.TeacherProfiles.SingleOrDefaultAsync(m => m.Id == id);
            _context.TeacherProfiles.Remove(teacherProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherProfileExists(int id)
        {
            return _context.TeacherProfiles.Any(e => e.Id == id);
        }
    }
}
