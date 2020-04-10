using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<StudentEnrollment> GetStudentEnrollments();
    }
}
