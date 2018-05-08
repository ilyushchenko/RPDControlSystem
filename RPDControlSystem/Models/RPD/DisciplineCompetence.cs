using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models.RPD
{
    public class DisciplineCompetence : CompetenceInfo
    {
        public string DisciplineCode { get; set; }
        public string PlanCode { get; set; }
        public DisciplineInfo Discipline { get; set; }
    }
}
