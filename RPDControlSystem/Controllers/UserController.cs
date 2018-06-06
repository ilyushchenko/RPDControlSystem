using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPDControlSystem.Models.RPD;
using RPDControlSystem.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RPDControlSystem.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        #region Private members

        private readonly DatabaseContext _context;
        private readonly UserManager<TeacherProfile> _userManager;

        #endregion

        #region Constructor

        public UserController(DatabaseContext context, UserManager<TeacherProfile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        #region User Account

        public async Task<IActionResult> Index()
        {
            string userName = User.Identity.Name;

            TeacherProfile profile = await _userManager.FindByNameAsync(userName);

            if (profile == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(profile);

            if (roles.Contains("admin"))
            {
                ViewBag.IsAdmin = true;
            }

            return View(profile);
        }

        // GET: TeacherProfiles/Edit/5
        public async Task<IActionResult> Edit()
        {

            string userName = User.Identity.Name;

            TeacherProfile profile = await _userManager.FindByNameAsync(userName);

            if (profile == null)
            {
                return NotFound();
            }

            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name", profile.DegreeId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Name", profile.PostId);
            return View(profile);
        }

        // POST: TeacherProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,MiddleName,LastName,DegreeId,PostId,PhotoId")] TeacherProfile teacherProfile)
        {
            if (id != teacherProfile.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                TeacherProfile user = await GetCurrentuserAsync();

                user.FirstName = teacherProfile.FirstName;
                user.LastName = teacherProfile.LastName;
                user.MiddleName = teacherProfile.MiddleName;
                user.DegreeId = teacherProfile.DegreeId;
                user.PostId = teacherProfile.PostId;

                try
                {
                    var result = await _userManager.UpdateAsync(user);
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

        private async Task<TeacherProfile> GetCurrentuserAsync()
        {
            string userName = User.Identity.Name;

            TeacherProfile profile = await _userManager.FindByNameAsync(userName);

            if (profile == null)
            {
                throw new Exception("Ошибка, при поиске пользователя");
            }

            return profile;
        }

        #endregion

        #region Helpers

        private bool TeacherProfileExists(string id)
        {
            return _context.TeacherProfiles.Any(e => e.Id == id);
        }

        #endregion
    }
}
