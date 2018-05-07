using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models
{
    public class DisciplineInfo
    {
        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }

        public string EducationPlanCode { get; set; }
        public EducationPlan EducationPlan { get; set; }

        public List<Competence> Competencies { get; set; }
    }
}
