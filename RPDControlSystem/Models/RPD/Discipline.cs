using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models.RPD
{
    public class Discipline
    {
        [Key]
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public List<DisciplineInfo> DisciplinesInfo { get; set; }
    }
}
