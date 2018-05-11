using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPDControlSystem.Models.RPD
{
    public class DisciplineInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Код дисциплины")]
        public string DisciplineCode { get; set; }
        public Discipline Discipline { get; set; }

        [Required]
        [Display(Name = "Код учебного плана")]
        public string PlanCode { get; set; }
        public Plan Plan { get; set; }

        [Required]
        [Display(Name = "Тип дисциплины")]
        public DisciplineType DisciplineType { get; set; }

        public List<DisciplineCompetence> Competencies { get; set; }

        [Display(Name = "Файл РПД")]
        public int? WorkPlanId { get; set; }
        public File WorkPlan { get; set; }

        [NotMapped]
        [Display(Name = "Наличие РПД")]
        public bool WorkPlanExist
        {
            get
            {
                return WorkPlanId != null;
            }
        }
    }
}
