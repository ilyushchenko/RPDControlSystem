using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPDControlSystem.Models.RPD;
using RPDControlSystem.Storage;

namespace RPDControlSystem.Controllers
{
    [Authorize]
    public class DirectionsController : Controller
    {
        #region Private Members

        private readonly DatabaseContext _context;

        #endregion

        #region Constructor

        public DirectionsController(DatabaseContext context)
        {
            _context = context;
        }

        #endregion

        #region Направления подготовки

        /// <summary>
        /// Выводит список направлений подготовки
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Direction.ToListAsync());
        }

        /// <summary>
        /// Выводит информацию о направлении подготовки
        /// </summary>
        /// <param name="id">Код направления подготовки</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await _context.Direction
                .SingleOrDefaultAsync(m => m.Code == id);
            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        /// <summary>
        /// GET: Создание направления подготовки
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Создание направления подготовки
        /// </summary>
        /// <param name="direction">Модель направления подготовки</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Code,Name,Qualification")] Direction direction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(direction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(direction);
        }

        /// <summary>
        /// GET: Редактирование направления подготовки
        /// </summary>
        /// <param name="id">Код направления подготовки</param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await _context.Direction.Include(c => c.Competencies).SingleOrDefaultAsync(m => m.Code == id);
            if (direction == null)
            {
                return NotFound();
            }
            return View(direction);
        }

        /// <summary>
        /// POST: Редактирование направления подготовки
        /// </summary>
        /// <param name="id">Код направления подготовки</param>
        /// <param name="direction">Модель направления подготовки</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id, [Bind("Code,Name,Qualification")] Direction direction)
        {
            if (id != direction.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectionExists(direction.Code))
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
            return View(direction);
        }

        /// <summary>
        /// Удаление направления подготовки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await _context.Direction
                .SingleOrDefaultAsync(m => m.Code == id);
            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        /// <summary>
        /// Подстверждение удаления направления подготовки
        /// </summary>
        /// <param name="id">Код направления подготовки</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var direction = await _context.Direction.SingleOrDefaultAsync(m => m.Code == id);
            _context.Direction.Remove(direction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectionExists(string id)
        {
            return _context.Direction.Any(e => e.Code == id);
        }

        #endregion

        #region Комепетенции

        /// <summary>
        /// Метод добавлет компетенцию к направлению подготовки
        /// </summary>
        /// <param name="competence">Модель комепетенции</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddCompetence([Bind("Id,DirectionCode,Code,Description")] Competence competence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit), new { id = competence.DirectionCode });
            }
            ViewData["DirectionCode"] = new SelectList(_context.Direction, "Code", "Code", competence.DirectionCode);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Отображает информацию, для редактирования выбранной компетенции
        /// </summary>
        /// <param name="id">Номер компетенции</param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditCompetence(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competence = await _context.Competence.SingleOrDefaultAsync(m => m.Id == id);
            if (competence == null)
            {
                return NotFound();
            }
            return View(competence);
        }

        /// <summary>
        /// Редактирует информацию, для выбранной компетенции
        /// </summary>
        /// <param name="id">Номер компетенции</param>
        /// <param name="competence">Модель компетенции</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditCompetence(int id, [Bind("Id,DirectionCode,Code,Description")] Competence competence)
        {
            if (id != competence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetenceExists(competence.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var direction = await _context.Direction.Include(c => c.Competencies).SingleOrDefaultAsync(m => m.Code == competence.DirectionCode);
                if (direction == null)
                {
                    return NotFound();
                }
                return View(nameof(Edit), direction);
            }

            ViewData["DirectionCode"] = new SelectList(_context.Direction, "Code", "Code", competence.DirectionCode);
            return View(competence);
        }

        /// <summary>
        /// Отображает диалог подтверждения удаления компетенции
        /// </summary>
        /// <param name="id">Номер компетенции</param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCompetence(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competence = await _context.Competence
                .Include(c => c.Direction)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (competence == null)
            {
                return NotFound();
            }

            return View(competence);
        }

        /// <summary>
        /// Удаляет компетенцию
        /// </summary>
        /// <param name="id">Номер компетенции</param>
        /// <returns></returns>
        [HttpPost, ActionName("DeleteCompetence")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteCompetenceConfirmed(int id)
        {
            var competence = await _context.Competence.SingleOrDefaultAsync(m => m.Id == id);
            _context.Competence.Remove(competence);
            await _context.SaveChangesAsync();

            var direction = await _context.Direction.Include(c => c.Competencies).SingleOrDefaultAsync(m => m.Code == competence.DirectionCode);
            if (direction == null)
            {
                return NotFound();
            }
            return View(nameof(Edit), direction);
        }

        private bool CompetenceExists(int id)
        {
            return _context.Competence.Any(e => e.Id == id);
        }

        #endregion

        #region Профиль

        /// <summary>
        /// Метод добавляет профиль подготовки, для направлдения подготовки
        /// </summary>
        /// <param name="profile">Модель профиля подготовки</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProfile([Bind("Code,Name,DirectionCode")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = profile.DirectionCode });
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
