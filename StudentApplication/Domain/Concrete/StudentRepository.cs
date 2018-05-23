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

        public void AddStudent(Student student)
        {
            if (context.Students.Where(x => x.Email == student.Email).FirstOrDefault() == null)
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
        }

        public void DeleteStudent(Student student)
        {
            context.Students.Remove(student);
            context.SaveChanges();
        }

        public Student GetStudent(string email)
        {
            Student student = context.Students.Where(x => x.Email == email).FirstOrDefault();
            return student;
        }

        public void UpdateStudent(Student student)
        {
            Student dbEntry = context.Students.Find(student.Email);
            context.Entry(dbEntry).CurrentValues.SetValues(student);
            context.SaveChanges();
        }
    }
}
