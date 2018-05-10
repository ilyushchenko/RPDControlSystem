using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPDControlSystem.Models.RPD
{
    public class Direction
    {
        [Key]
        [Display(Name = "Код направления подготовки")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Название направления подготовки")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Квалификация выпускника")]
        public int QualificationId { get; set; }
        public Qualification Qualification { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{Code} {Name}";
            }
        }

        public List<Profile> Profiles { get; set; }

        public List<Competence> Competencies { get; set; }
    }
}
