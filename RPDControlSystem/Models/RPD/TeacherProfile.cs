using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPDControlSystem.Models.RPD
{
    public class TeacherProfile
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Ученая степень")]
        public int DegreeId { get; set; }
        public Degree Degree { get; set; }

        [Required]
        [Display(Name = "Должность")]
        public int PostId { get; set; }
        public Post Post { get; set; }

        [Display(Name = "Фотография")]
        public int? PhotoId { get; set; }
        public File Photo { get; set; }

        public List<DisciplineInfo> Disciplines { get; set; }

        [NotMapped]
        [Display(Name = "ФИО")]
        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName} {MiddleName}";
            }
        }
    }
}
