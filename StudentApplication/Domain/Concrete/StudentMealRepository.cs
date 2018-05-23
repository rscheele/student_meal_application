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

        public bool CheckForExisitingStudentMeal(int studentId, int mealId)
        {
            StudentMeal studentMeal = context.StudentMeals.Where(x => x.MealId == mealId && x.Student.StudentId == studentId).FirstOrDefault();
            if (studentMeal != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void DeleteStudentMeal(StudentMeal studentMeal)
        {
            context.StudentMeals.Remove(studentMeal);
            context.SaveChanges();
        }

        public StudentMeal GetStudentMeal(StudentMeal studentMeal)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StudentMeal> GetStudentMealsForMeal(Meal meal)
        {
            IEnumerable<StudentMeal> studentMeals = context.StudentMeals.Where(x => x.MealId == meal.MealId);
            return studentMeals;
        }

        public IEnumerable<StudentMeal> GetStudentMealsForStudent(Student student)
        {
            IEnumerable<StudentMeal> studentMeals = context.StudentMeals.Where(x => x.StudentID == student.StudentId);
            return studentMeals;
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
