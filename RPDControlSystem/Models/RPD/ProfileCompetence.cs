using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models.RPD
{
    public class ProfileCompetence : CompetenceInfo
    {
        public string ProfileCode { get; set; }
        public Profile Profile { get; set; }
    }
}
