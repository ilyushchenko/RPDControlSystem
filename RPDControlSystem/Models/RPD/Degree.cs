using System.ComponentModel.DataAnnotations;

namespace RPDControlSystem.Models.RPD
{
    public class Degree
    {
        [Required]
        [Display(Name = "Идентификатор ученой степени")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ученая степень")]
        public string Name { get; set; }
    }
}
