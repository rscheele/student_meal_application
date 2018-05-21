using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IStudentRepository
    {
        void AddStudent(Student student);
        Student GetStudent(Student student);
        void DeleteStudent(Student student);
        void UpdateStudent(Student student);
    }
}
