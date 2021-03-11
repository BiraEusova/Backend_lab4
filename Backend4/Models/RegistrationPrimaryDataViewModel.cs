using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend4.Models
{
    public class RegistrationPrimaryDataViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Day { get; set; }
        [Required]
        public string Month { get; set; }
        [Required]
        public int Year { get; set; }
        //public Birthday Birthday { get; set; }
        [Required]
        public String Gender { get; set; }
        public bool UserExisted { get; set; }

    }
}
