﻿using System;
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
        public int ClientId { get; set; }


        public bool IsActiveAccount { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(11)]
        public string IDNumber { get; set; }

        [Range(typeof(DateTime), "1/1/1870", "1/1/2022" ,ErrorMessage = "Unsupported Date of birth")]
        public DateTime DateOfBirth { get; set; }


        public ClientContactInfo ClientContactInfo { get; set; }

        public ICollection<Car> Cars { get; set; }

    }
}