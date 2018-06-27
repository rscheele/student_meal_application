using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    // Interface for student repository
    public interface IStudentRepository
    {
        void AddStudent(Student student);
        Student GetStudent(string email);
    }
}
