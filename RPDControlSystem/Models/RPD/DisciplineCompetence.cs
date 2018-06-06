using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models.RPD
{
    public class DisciplineCompetence
    {
        [Required]
        [Display(Name = "Идентификатор дисциплины")]
        public int DisciplineInfoId { get; set; }
        public DisciplineInfo DisciplineInfo { get; set; }

        [Required]
        [Display(Name = "Идентификатор компетенции")]
        public int CompetenceId { get; set; }
        public Competence Competence { get; set; }
    }
}
