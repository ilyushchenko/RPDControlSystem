using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models.RPD
{
    public class DirectionCompetence : CompetenceInfo
    {
        public string DirectionCode { get; set; }
        public Direction Direction { get; set; }
    }
}
