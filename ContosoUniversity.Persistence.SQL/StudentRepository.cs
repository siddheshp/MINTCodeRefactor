using ContosoUniversity.Interfaces;
using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Persistence.SQL
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        SchoolContext _context;
        public StudentRepository(SchoolContext context)
            : base(context)
        {
            _context = context;
        }
        public IEnumerable<StudentEnrollment> GetStudentEnrollments()
        {
            return _context.Students.GroupBy(s => s.EnrollmentDate)
                .Select(x => new StudentEnrollment
                {
                    EnrollmentDate = x.Key,
                    StudentCount = x.Count()
                })
                .OrderBy(s => s.EnrollmentDate);
        }
    }
}
