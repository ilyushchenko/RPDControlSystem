using System.ComponentModel.DataAnnotations;

namespace RPDControlSystem.Models.RPD
{
    public class ProfileCompetence
    {
        [Required]
        [Display(Name = "Код профиля")]
        public string ProfileCode { get; set; }
        public Profile Profile { get; set; }

        [Required]
        [Display(Name = "Код компетенции")]
        public int CompetenceId { get; set; }
        public Competence Competence { get; set; }
    }
}
