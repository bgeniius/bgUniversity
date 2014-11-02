using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BgeniiusUniversity.Models
{
    public class Student : Person
    {
        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }
        
        [Required(ErrorMessage = "Select your birth country")]
        [Display(Name = "Birth Country")]
        public string BirthCountry { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}