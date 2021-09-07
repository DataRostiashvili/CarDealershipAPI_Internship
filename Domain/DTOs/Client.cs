using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class Client : BaseDto
    {

        public string Name { get; set; }
        public string Surname { get; set; }

        public string IDNumber { get; set; }

        public DateTime DateOfBirth { get; set; }


    }
}
