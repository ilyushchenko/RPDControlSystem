using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPDControlSystem.Models.RPD
{
    public class Competence
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{Code} {Description}";
            }
        }

    }
}
