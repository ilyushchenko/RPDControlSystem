using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RPDControlSystem.Models.RPD;

namespace RPDControlSystem.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Старый пароль")]
        /// <summary>
        /// Старый пароль пользователя
        /// </summary>
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        /// <summary>
        /// Новый пароль пользователя
        /// </summary>
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Повтор нового пароля")]
        [Compare(nameof(NewPassword), ErrorMessage = "Введенные пароли не совпадают")]
        /// <summary>
        /// Повтор ввода нового пароля пользователя
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}