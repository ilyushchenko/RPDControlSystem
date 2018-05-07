using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RPDControlSystem.Models
{
    public class File
    {
        public int Id { get; set; }

        public string Directory { get; set; }
        public string Name { get; set; }
        public string BaseName { get; set; }
        
        [NotMapped]
        public string FullPath {
            get { return $"{Directory}{Name}"; }
        }

    }
}
