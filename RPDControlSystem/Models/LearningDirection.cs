using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models
{
    public class LearningDirection
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int QualificationId { get; set; }
        public Qualification Qualification { get; set; }

        public List<LearningProfile> LearningProfiles { get; set; }

        public List<Competence> Competencies { get; set; }
    }
}
