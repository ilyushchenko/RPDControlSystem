using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RPDControlSystem.Models.RPD
{
    public class Direction
    {
        [Key]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required] 
        public int QualificationId { get; set; }
        public Qualification Qualification { get; set; }

        public List<Profile> Profiles { get; set; }

        public List<DirectionCompetence> Competencies { get; set; }
    }
}
