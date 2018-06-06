using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPDControlSystem.Models.RPD;
using RPDControlSystem.Storage;
using RPDControlSystem.ViewModels;

namespace RPDControlSystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly DatabaseContext _context;

        public ReportController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FindCompetenceExceeding()
        {
            var competence = _context.DisciplineInfo.Where(discipline => discipline.Competencies.Count > 3).ToList();
            return View();
        }

        public IActionResult FindPlanWithoutCompetence()
        {
            var reportedItems = new List<PlanCompetenceListViewModel>();

            var plans = _context.Plan
                .Include(p => p.Profile).ThenInclude(pc => pc.Competencies).ThenInclude(c => c.Competence)
                .Include(d => d.Disciplines).ThenInclude(dc => dc.Competencies).ThenInclude(c => c.Competence)
                .ToList();
            foreach (var plan in plans)
            {
                var fgosCompetence = plan.Profile.Competencies.Select(c => c.Competence).ToList();

                foreach (var discipline in plan.Disciplines)
                {
                    var disciplineCompetence = discipline.Competencies.Select(c => c.Competence).ToList();

                    fgosCompetence = fgosCompetence.Except(disciplineCompetence).ToList();
                }

                if(fgosCompetence.Count > 0)
                {
                    reportedItems.Add(new PlanCompetenceListViewModel
                    {
                        Plan = plan,
                        Competences = fgosCompetence
                    });
                }
            }
            return View(reportedItems);
        }

        public IActionResult FindTeacherByPlan(bool exist)
        {
            var teachersToReport = new List<TeacherProfile>();
            var teachers = _context.TeacherProfiles.Include(td => td.Disciplines).ThenInclude(d => d.Discipline).ToList();

            teachers.Sort((x,y) => x.FullName.CompareTo(y.FullName));

            foreach (var teacher in teachers)
            {
                var disciplines = teacher.Disciplines.Where(d => d.WorkPlanExist == exist).ToList();
                if (disciplines.Count > 0)
                {
                    teacher.Disciplines = disciplines;
                    teachersToReport.Add(teacher);
                }
            }

            ViewBag.IsExist = exist;
            return View(teachersToReport);
        }

        public IActionResult FindDisciplinesWithoutRpd()
        {
            //var teachersToReport = new List<TeacherProfile>();
            var disciplines = _context.DisciplineInfo.Include(d => d.Discipline).Where(wp => wp.WorkPlanExist == false).ToList();

            disciplines.Sort((x, y) => x.PlanCode.CompareTo(y.PlanCode));

            return View(disciplines);
        }

        public IActionResult FindDisciplinesWithRpd(SortType type = SortType.Plan)
        {
            var disciplines = _context.DisciplineInfo
                .Include(d => d.Discipline)
                .Include(w => w.WorkPlan)
                .Include(p => p.Plan)
                    .ThenInclude(pr => pr.Profile)
                        .ThenInclude(d => d.Direction)
                .Where(wp => wp.WorkPlanExist).ToList();
            switch (type)
            {
                case SortType.Plan:
                    disciplines.Sort((x, y) => x.PlanCode.CompareTo(y.PlanCode));
                    break;
                case SortType.Qualification:
                    disciplines.Sort((x, y) => x.Plan.Profile.Direction.Qualification.CompareTo(y.Plan.Profile.Direction.Qualification));
                    break;
                case SortType.Profile:
                    disciplines.Sort((x, y) => x.Plan.ProfileCode.CompareTo(y.Plan.ProfileCode));
                    break;
                default:
                    break;
            }
            ViewBag.Type = type;
            return View(disciplines);

        }
    }
}