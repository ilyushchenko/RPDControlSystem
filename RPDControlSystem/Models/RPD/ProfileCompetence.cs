using System.ComponentModel.DataAnnotations;

namespace RPDControlSystem.Models.RPD
{
    public class ProfileCompetence
    {
        [Required]
        public string ProfileCode { get; set; }
        public Profile Profile { get; set; }

        [Required]
        public int CompetenceId { get; set; }
        public Competence Competence { get; set; }
    }
}
