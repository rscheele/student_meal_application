﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    // Interface for studentmeal repository
    public interface IStudentMealRepository
    {
        void AddStudentMeal(StudentMeal studentMeal);
        void DeleteStudentMeal(StudentMeal studentMeal);
        void DeleteStudentMeal(int studentId, int mealId);
        IEnumerable<StudentMeal> GetStudentMealsForMeal(Meal meal);
        IEnumerable<StudentMeal> GetStudentMealsForStudent(Student student);
        bool CheckForExisitingStudentMeal(int studentId, int mealId);

    }
}