using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IHostingEnvironment _appEnvironment;

        public DisciplineInfoesController(DatabaseContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
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
        [Authorize]
        public IActionResult Create()
        {
            ViewData["DisciplineCode"] = new SelectList(_context.Discipline, "Code", "Code");
            ViewData["PlanCode"] = new SelectList(_context.Plan, "Code", "Code");
            ViewData["WorkPlanId"] = new SelectList(_context.Set<Models.File>(), "Id", "BaseName");
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
            ViewData["WorkPlanId"] = new SelectList(_context.Set<Models.File>(), "Id", "BaseName", disciplineInfo.WorkPlanId);
            return View(disciplineInfo);
        }

        // GET: DisciplineInfoes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplineInfo = await _context.DisciplineInfo
                .Include(p => p.Plan)
                    .ThenInclude(p => p.Profile)
                        .ThenInclude(p => p.Competencies)
                            .ThenInclude(c => c.Competence)
                .Include(c => c.Competencies)
                .Include(w => w.WorkPlan)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (disciplineInfo == null)
            {
                return NotFound();
            }

            var profileCompetence = disciplineInfo.Plan.Profile.Competencies.Select(c => c.Competence);

            //ViewBag.CompetenceId

            var result = profileCompetence.Except(disciplineInfo.Competencies.Select(c => c.Competence));

            ViewData["CompetenceId"] = new SelectList(result, "Id", "FullName");
            ViewData["DisciplineCode"] = new SelectList(_context.Discipline, "Code", "Code", disciplineInfo.DisciplineCode);
            ViewData["PlanCode"] = new SelectList(_context.Plan, "Code", "Code", disciplineInfo.PlanCode);
            ViewData["WorkPlanId"] = new SelectList(_context.Set<Models.File>(), "Id", "BaseName", disciplineInfo.WorkPlanId);
            ViewData["TeacherId"] = new SelectList(_context.TeacherProfiles, "Id", "FullName", disciplineInfo.TeacherProfileId);

            return View(disciplineInfo);
        }

        // POST: DisciplineInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DisciplineCode,PlanCode,DisciplineType,WorkPlanId,TeacherProfileId")] DisciplineInfo disciplineInfo)
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
            ViewData["WorkPlanId"] = new SelectList(_context.Set<Models.File>(), "Id", "BaseName", disciplineInfo.WorkPlanId);
            return View(disciplineInfo);
        }

        // GET: DisciplineInfoes/Delete/5
        [Authorize]
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
        [Authorize]
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDisciplineCompetence([Bind("DisciplineInfoId,CompetenceId")] DisciplineCompetence disciplineCompetence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplineCompetence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit), new { id = disciplineCompetence.DisciplineInfoId });
            }
            ViewData["CompetenceId"] = new SelectList(_context.Competence, "Id", "Code", disciplineCompetence.CompetenceId);
            ViewData["DisciplineInfoId"] = new SelectList(_context.DisciplineInfo, "Id", "DisciplineCode", disciplineCompetence.DisciplineInfoId);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadRPDFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                int id = int.Parse(Request.Form["id"]);

                var disciplineInfo = await _context.DisciplineInfo.Include(f => f.WorkPlan).SingleOrDefaultAsync(m => m.Id == id);
                if (disciplineInfo == null)
                {
                    return NotFound();
                }

                if (Path.GetExtension(uploadedFile.FileName).ToLower() != ".pdf")
                {
                    ModelState.AddModelError("WorkPlan", "Недопустимый формат (допускается .pdf)");
                    return View(nameof(Edit), disciplineInfo);
                }

                FileManager fm = new FileManager(_context, _appEnvironment);

                Models.File file = fm.SaveFile(uploadedFile);

                if(disciplineInfo.WorkPlanExist)
                {
                    fm.DeleteFile(disciplineInfo.WorkPlan);
                }

                disciplineInfo.WorkPlan = file;

                _context.SaveChanges();

                return View(nameof(Edit), disciplineInfo);
            }

            return RedirectToAction("Index");
        }
    }
}
