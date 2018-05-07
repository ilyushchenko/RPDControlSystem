using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models
{
    public class LearningProfile
    {
        public string Code { get; set; }

        public List<EducationPlan> EducationPlans { get; set; }

        public List<Competence> Competencies { get; set; }

        public string LearningDirectionCode { get; set; }
        public LearningDirection LearningDirection { get; set; }
    }
}
