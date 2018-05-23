using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IStudentMealRepository
    {
        void AddStudentMeal(StudentMeal studentMeal);
        void UpdateStudentMeal(StudentMeal studentMeal);
        StudentMeal GetStudentMeal(StudentMeal studentMeal);
        void DeleteStudentMeal(StudentMeal studentMeal);
        IEnumerable<StudentMeal> GetStudentMealsForMeal(Meal meal);
        IEnumerable<StudentMeal> GetStudentMealsForStudent(Student student);
        IEnumerable<StudentMeal> GetStudentMealsForStudentCook(Student student);
        bool CheckForExisitingStudentMeal(int studentId, int mealId);

    }
}
