using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models
{
    public class EducationPlan
    {
        public string Code { get; set; }

        public int EducationFormId { get; set; }
        public EducationForm EducationForm { get; set; }

        public string LearningProfileCode { get; set; }
        public LearningProfile LearningProfile { get; set; }
    }
}
