using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPDControlSystem.Models.RPD;

namespace RPDControlSystem.ViewModels
{
    public class PlanCompetenceListViewModel
    {
        public Plan Plan { get; set; }
        public List<Competence> Competences { get; set; }
    }
}
