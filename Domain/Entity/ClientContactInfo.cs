using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Entity
{
    public class ClientContactInfo
    {
        [Required]
        [RegularExpression(@"^\d{9}$")]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^.+@.+(\.).+$")]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
