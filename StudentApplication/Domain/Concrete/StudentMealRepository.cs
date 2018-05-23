using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class StudentMealRepository : IStudentMealRepository
    {
        private EFDBContextStudent context = new EFDBContextStudent();

        public void AddStudentMeal(StudentMeal studentMeal)
        {
            context.StudentMeals.Add(studentMeal);
            context.SaveChanges();
        }

        public void DeleteStudentMeal(StudentMeal studentMeal)
        {
            throw new NotImplementedException();
        }

        public StudentMeal GetStudentMeal(StudentMeal studentMeal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentMeal> GetStudentMealsForMeal(Meal meal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentMeal> GetStudentMealsForStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentMeal> GetStudentMealsForStudentCook(Student student)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudentMeal(StudentMeal studentMeal)
        {
            throw new NotImplementedException();
        }
    }
}
