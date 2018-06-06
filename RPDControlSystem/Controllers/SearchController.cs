using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPDControlSystem.Storage;
using RPDControlSystem.ViewModels;

namespace RPDControlSystem.Controllers
{
    public class SearchController : Controller
    {
        private readonly DatabaseContext _context;

        public SearchController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FindAll(string query)
        {
            FullSearchModel model = new FullSearchModel();

            if (query == null)
                return View(model);

            var planResult = _context.Plan
                .Include(d => d.Profile)
                .Where(plan => plan.Search(query))
                .ToList();

            var profileResult = _context.Profile
                .Include(d => d.Direction)
                .Where(profile => profile.Search(query))
                .ToList();

            var disciplines = _context.DisciplineInfo
                .Include(d => d.Discipline)
                .Include(d => d.Plan).ToList();

            var disciplinesResult = disciplines
                .Where(discipline => discipline.Search(query)).ToList();

            model.Profiles = profileResult;
            model.Plans = planResult;
            model.Disciplines = disciplinesResult;


            return View(model);
        }
    }
}