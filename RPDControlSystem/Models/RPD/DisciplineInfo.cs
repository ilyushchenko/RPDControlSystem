using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPDControlSystem.Models.RPD
{
    public class DisciplineInfo
    {
        [Key]
        [Display(Name = "Идентификатор дисциплины")]
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

        [Display(Name = "Профиль преподавателя")]
        public string TeacherProfileId { get; set; }
        public TeacherProfile TeacherProfile { get; set; }

        [NotMapped]
        [Display(Name = "Наличие РПД")]
        public bool WorkPlanExist
        {
            get
            {
                return WorkPlanId != null;
            }
        }

        [NotMapped]
        [Display(Name = "Код")]
        public string FullCode
        {
            get
            {
                return $"{DisciplineCode}.{Id}";
            }
        }

        public bool Search(string query)
        {
            query = query.ToLower();

            bool codeContains = DisciplineCode.Contains(query);
            bool disciplineContains = Discipline != null && Discipline.Name.ToLower().Contains(query);
            bool planContains = PlanCode.Contains(query);

            return codeContains || disciplineContains || planContains;
        }
    }
}
