using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPDControlSystem.Models.RPD;
using RPDControlSystem.Storage;
using RPDControlSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace RPDControlSystem.Controllers
{
    [Route("Admin")]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        #region Privare members

        private readonly DatabaseContext _context;
        private readonly UserManager<TeacherProfile> _userManager;

        #endregion

        #region Constructor

        public AdminController(DatabaseContext context, UserManager<TeacherProfile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        #region Admin

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Ученые степени

        [Route("Degrees")]
        public async Task<IActionResult> GetDegrees()
        {
            return View(await _context.Degrees.ToListAsync());
        }

        [Route("Degrees/Create")]
        public IActionResult CreateDegree()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Degrees/Create")]
        public async Task<IActionResult> CreateDegree([Bind("Id,Name")] Degree degree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(degree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(degree);
        }

        [Route("Degrees/{id}/Edit")]
        public async Task<IActionResult> EditDegree(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degree = await _context.Degrees.SingleOrDefaultAsync(m => m.Id == id);
            if (degree == null)
            {
                return NotFound();
            }
            return View(degree);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Degrees/{id}/Edit")]
        public async Task<IActionResult> EditDegree(int id, [Bind("Id,Name")] Degree degree)
        {
            if (id != degree.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(degree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DegreeExists(degree.Id))
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
            return View(degree);
        }

        [Route("Degrees/{id}/Delete")]
        public async Task<IActionResult> DeleteDegree(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degree = await _context.Degrees
                .SingleOrDefaultAsync(m => m.Id == id);
            if (degree == null)
            {
                return NotFound();
            }

            return View(degree);
        }

        // POST: Degrees/Delete/5
        [HttpPost, ActionName("DeleteDegree")]
        [ValidateAntiForgeryToken]
        [Route("Degrees/{id}/Delete")]
        public async Task<IActionResult> DeleteDegreeConfirmed(int id)
        {
            var degree = await _context.Degrees.SingleOrDefaultAsync(m => m.Id == id);
            _context.Degrees.Remove(degree);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DegreeExists(int id)
        {
            return _context.Degrees.Any(e => e.Id == id);
        }

        #endregion

        #region Должность

        [Route("Posts")]
        public async Task<IActionResult> GetPosts()
        {
            return View(await _context.Posts.ToListAsync());
        }

        // GET: Posts/Create
        [Route("Posts/Create")]
        public IActionResult CreatePost()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Posts/Create")]
        public async Task<IActionResult> CreatePost([Bind("Id,Name")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [Route("Posts/{id}/Edit")]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [Route("Posts/{id}/Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, [Bind("Id,Name")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(post);
        }

        [Route("Posts/{id}/Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [Route("Posts/{id}/Delete")]
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePostConfirmed(int id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        #endregion

        #region Преподаватели
        [Route("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var databaseContext = _context.TeacherProfiles.Include(t => t.Degree).Include(t => t.Photo).Include(t => t.Post);
            return View(await databaseContext.ToListAsync());
        }

        [Route("Users/{id}")]
        public async Task<IActionResult> DetailsUser(string id)
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
        [Route("Users/Create")]
        public IActionResult CreateUser()
        {
            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name");
            return View();
        }

        /// <summary>
        /// Отвечает за создание пользователя
        /// </summary>
        /// <param name="teacher">Модель пользователя</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Users/Create")]
        public async Task<IActionResult> CreateUser([Bind("Email,Password,Login,FirstName,MiddleName,LastName,DegreeId,PostId,Roles")] CreateTeacherViewModel teacher)
        {
            if (ModelState.IsValid)
            {
                var userModel = new TeacherProfile()
                {
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    MiddleName = teacher.MiddleName,
                    DegreeId = teacher.DegreeId,
                    PostId = teacher.PostId,
                    Email = teacher.Email,
                    UserName = teacher.Login
                };
                var result = await _userManager.CreateAsync(userModel, teacher.Password);
                if (result.Succeeded)
                {
                    if (teacher.Roles.Contains("admin"))
                        await _userManager.AddToRoleAsync(userModel, "admin");
                    if (teacher.Roles.Contains("teacher"))
                        await _userManager.AddToRoleAsync(userModel, "teacher");
                    await _userManager.AddToRoleAsync(userModel, "user");

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name", teacher.DegreeId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", teacher.PostId);
            return View(teacher);
        }

        [Route("Users/{id}/Edit")]
        public async Task<IActionResult> EditUser(string id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Users/{id}/Edit")]
        public async Task<IActionResult> EditUser(string id, [Bind("Id,FirstName,MiddleName,LastName,DegreeId,PostId,PhotoId")] TeacherProfile teacherProfile)
        {
            if (id != teacherProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);

                user.FirstName = teacherProfile.FirstName;
                user.MiddleName = teacherProfile.MiddleName;
                user.LastName = teacherProfile.LastName;
                user.DegreeId = teacherProfile.DegreeId;
                user.PostId = teacherProfile.PostId;
                user.PhotoId = teacherProfile.PhotoId;

                try
                {
                    await _userManager.UpdateAsync(user);
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
                return RedirectToAction(nameof(GetUsers));
            }
            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name", teacherProfile.DegreeId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", teacherProfile.PostId);
            return View(teacherProfile);
        }

        [Route("Users/{id}/Delete")]
        public async Task<IActionResult> DeleteUser(string id)
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

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        [Route("Users/{id}/Delete")]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var teacherProfile = await _context.TeacherProfiles.SingleOrDefaultAsync(m => m.Id == id);
            _context.TeacherProfiles.Remove(teacherProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #region Helpers

        private bool TeacherProfileExists(string id)
        {
            return _context.TeacherProfiles.Any(e => e.Id == id);
        }

        #endregion

        #endregion

        #region Дисциплины
        [Route("Disciplines")]
        public async Task<IActionResult> GetDisciplines()
        {
            return View(await _context.Discipline.ToListAsync());
        }

        [Route("Disciplines/{id}")]
        public async Task<IActionResult> DetailsDiscipline(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline = await _context.Discipline
                .SingleOrDefaultAsync(m => m.Code == id);
            if (discipline == null)
            {
                return NotFound();
            }

            return View(discipline);
        }

        [Route("Disciplines/Create")]
        public IActionResult CreateDiscipline()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Disciplines/Create")]
        public async Task<IActionResult> CreateDiscipline([Bind("Code,Name")] Discipline discipline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discipline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discipline);
        }

        [Route("Disciplines/{id}/Edit")]
        public async Task<IActionResult> EditDiscipline(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline = await _context.Discipline.SingleOrDefaultAsync(m => m.Code == id);
            if (discipline == null)
            {
                return NotFound();
            }
            return View(discipline);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Disciplines/{id}/Edit")]
        public async Task<IActionResult> EditDiscipline(string id, [Bind("Code,Name")] Discipline discipline)
        {
            if (id != discipline.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discipline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisciplineExists(discipline.Code))
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
            return View(discipline);
        }

        [Route("Disciplines/{id}/Delete")]
        public async Task<IActionResult> DeleteDiscipline(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discipline = await _context.Discipline
                .SingleOrDefaultAsync(m => m.Code == id);
            if (discipline == null)
            {
                return NotFound();
            }

            return View(discipline);
        }

        [HttpPost, ActionName("DeleteDiscipline")]
        [ValidateAntiForgeryToken]
        [Route("Disciplines/{id}/Delete")]
        public async Task<IActionResult> DeleteDisciplineConfirmed(string id)
        {
            var discipline = await _context.Discipline.SingleOrDefaultAsync(m => m.Code == id);
            _context.Discipline.Remove(discipline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #region Helpers

        private bool DisciplineExists(string id)
        {
            return _context.Discipline.Any(e => e.Code == id);
        }

        #endregion

        #endregion
    }
}