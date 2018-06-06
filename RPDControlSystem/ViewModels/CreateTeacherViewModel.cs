using RPDControlSystem.Models.RPD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.ViewModels
{
    public class CreateTeacherViewModel
    {
        [Required]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Ученая степень")]
        public int? DegreeId { get; set; }

        [Display(Name = "Должность")]
        public int? PostId { get; set; }

        [Display(Name = "Роли пользователя")]
        public List<string> Roles { get; set; }
    }
}
