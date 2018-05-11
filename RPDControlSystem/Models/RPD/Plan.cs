using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPDControlSystem.Models.RPD
{
    public class Plan
    {
        [Key]
        [Required]
        [Display(Name = "Код плана")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Форма обучения")]
        public EducationForm EducationForm { get; set; }

        [Required]
        [Display(Name = "Код профиля обучения")]
        public string ProfileCode { get; set; }
        public Profile Profile { get; set; }

        public List<DisciplineInfo> Disciplines { get; set; }
    }
}
