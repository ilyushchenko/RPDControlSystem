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
using RPDControlSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RPDControlSystem.ViewModels;

namespace RPDControlSystem.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        #region Private members

        private readonly DatabaseContext _context;
        private readonly UserManager<TeacherProfile> _userManager;
        private readonly IHostingEnvironment _appEnvironment;

        #endregion

        #region Constructor

        public UserController(DatabaseContext context, UserManager<TeacherProfile> userManager, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _appEnvironment = appEnvironment;
        }

        #endregion

        #region User Account

        public async Task<IActionResult> Index()
        {
            TeacherProfile profile = await GetCurrentuserAsync();

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

            TeacherProfile profile = await GetCurrentuserAsync();

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

        [HttpPost]
        public async Task<IActionResult> UploadProfilePhoto(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                FileManager _fileManager = new FileManager(_context, _appEnvironment);

                TeacherProfile profile = await GetCurrentuserAsync();

                if (profile != null)
                {
                    if (profile.PhotoId != null)
                    {
                        _fileManager.DeleteFile(profile.Photo);
                    }

                    File photo = _fileManager.SaveFile(uploadedFile);

                    profile.Photo = photo;
                    _context.TeacherProfiles.Update(profile);
                    _context.SaveChanges();

                    return View(nameof(Edit), profile);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            if (ModelState.IsValid)
            {
                TeacherProfile user = await GetCurrentuserAsync();
                if (user != null)
                {
                    ChangePasswordViewModel changeViewModel = new ChangePasswordViewModel()
                    {
                        Id = user.Id
                    };
                    return View(changeViewModel);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                TeacherProfile user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
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
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeEmail(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                TeacherProfile user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
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
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }

        #endregion

        #region Helpers

        private async Task<TeacherProfile> GetCurrentuserAsync()
        {
            string userName = User.Identity.Name;

            TeacherProfile profile = await _userManager.FindByNameAsync(userName);

            if (profile == null)
            {
                throw new Exception("Ошибка, при поиске пользователя");
            }

            if (profile.PhotoId != null)
            {
                File photo = _context.Files.FirstOrDefault(f => f.Id == profile.PhotoId);
                if (photo != null)
                {
                    profile.Photo = photo;
                }
            }

            return profile;
        }

        private bool TeacherProfileExists(string id)
        {
            return _context.TeacherProfiles.Any(e => e.Id == id);
        }

        #endregion
    }
}
