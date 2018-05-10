using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPDControlSystem.Models.RPD
{
    public class Profile
    {
        [Key]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DirectionCode { get; set; }
        public Direction Direction { get; set; }

        public List<Plan> Plans { get; set; }

        public List<ProfileCompetence> Competencies { get; set; }
    }
}
