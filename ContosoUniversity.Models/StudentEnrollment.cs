using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class StudentEnrollment
    {
        public DateTime EnrollmentDate { get; set; }

        public int StudentCount { get; set; }
    }
}