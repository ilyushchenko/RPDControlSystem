using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Directory { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BaseName { get; set; }
        
        [NotMapped]
        public string FullPath {
            get { return $"{Directory}{Name}"; }
        }

    }
}
