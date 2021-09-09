using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.APIModels
{
    public class Client
    {

        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Surname { get; set; }

        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Invalid IDNumber")]
        public string IDNumber { get; set; }

        [Range(typeof(DateTime), "1/1/1870", "1/1/2022", ErrorMessage = "Unsupported Date of birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression(@"^\d{9}$")]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^.+@.+(\.).+$")]
        public string Email { get; set; }
        [MaxLength(1024)]
        public string Address { get; set; }



    }
}
