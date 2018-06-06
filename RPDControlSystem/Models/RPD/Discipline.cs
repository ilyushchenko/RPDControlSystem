using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPDControlSystem.Models.RPD
{
    public class Discipline
    {
        [Key]
        [Required]
        [Display(Name = "Код дисциплины")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Название дисциплины")]
        public string Name { get; set; }

        public List<DisciplineInfo> DisciplinesInfo { get; set; }
    }
}
