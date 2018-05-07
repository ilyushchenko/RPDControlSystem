using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models
{
    public class Discipline
    {
        public string Code { get; set; }

        public string Name { get; set; }

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
