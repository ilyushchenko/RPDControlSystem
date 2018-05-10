using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPDControlSystem.Models.RPD
{
    public class Profile
    {
        [Key]
        [Display(Name = "Код профиля подготовки")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Название профиля подготовки")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Код направления подготовки")]
        public string DirectionCode { get; set; }
        public Direction Direction { get; set; }

        public List<Plan> Plans { get; set; }

        public List<ProfileCompetence> Competencies { get; set; }
    }
}
