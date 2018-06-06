using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPDControlSystem.Models.RPD
{
    public class Competence
    {
        [Key]
        [Display(Name = "Идентификатор компетенции")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Код направления подготовки")]
        public string DirectionCode { get; set; }
        public Direction Direction { get; set; }

        [Required]
        [Display(Name = "Код Компетенции")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Описание компетенции")]
        public string Description { get; set; }

        public List<DisciplineCompetence> Disciplines { get; set; }
        public List<ProfileCompetence> Profiles { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{Code} {Description}";
            }
        }

    }
}
