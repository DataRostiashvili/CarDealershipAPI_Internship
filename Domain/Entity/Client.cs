using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entity
{
    public class Client
    {
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(11)]
        public string IDNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
}
