﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPDControlSystem.Models.RPD
{
    public class DisciplineInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DisciplineCode { get; set; }
        public Discipline Discipline { get; set; }

        [Required]
        public string PlanCode { get; set; }
        public Plan Plan { get; set; }

        [Required]
        public DisciplineType DisciplineType { get; set; }

        public List<DisciplineCompetence> Competencies { get; set; }

        public int? WorkPlanId { get; set; }
        public File WorkPlan { get; set; }

        [NotMapped]
        public bool WorkPlanExist
        {
            get
            {
                return WorkPlanId != null;
            }
        }
    }
}