using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPDControlSystem.Models.RPD
{
    public class Plan
    {
        [Key]
        public string Code { get; set; }

        [Required]
        public int EducationFormId { get; set; }
        public EducationForm EducationForm { get; set; }

        [Required]
        public string ProfileCode { get; set; }
        public Profile Profile { get; set; }

        public List<DisciplineInfo> Disciplines { get; set; }
    }
}
