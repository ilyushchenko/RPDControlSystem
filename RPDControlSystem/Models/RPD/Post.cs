using System.ComponentModel.DataAnnotations;

namespace RPDControlSystem.Models.RPD
{
    public class Post
    {
        [Required]
        [Display(Name = "Идентификатор должности")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Должность")]
        public string Name { get; set; }
    }
}
