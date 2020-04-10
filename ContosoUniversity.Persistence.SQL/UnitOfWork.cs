using ContosoUniversity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Persistence.SQL
{
    public class UnitOfWork : IUnitOfWork
    {
        SchoolContext _context;
        public UnitOfWork(SchoolContext context, IStudentRepository students)
        {
            _context = context;
            Students = students;
        }

        public IStudentRepository Students { get; private set; }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
