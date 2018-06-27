using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class StudentRepository : IStudentRepository
    {
        private EFDBContextStudent context = new EFDBContextStudent();

        // Add student to database
        public void AddStudent(Student student)
        {
            if (context.Students.Where(x => x.Email == student.Email).FirstOrDefault() == null)
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
        }

        // Get student from database
        public Student GetStudent(string email)
        {
            Student student = context.Students.Where(x => x.Email == email).FirstOrDefault();
            return student;
        }
    }
}
