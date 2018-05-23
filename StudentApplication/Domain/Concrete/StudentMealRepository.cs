﻿using Domain.Abstract;
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

        // Add studentmeal to database
        public void AddStudentMeal(StudentMeal studentMeal)
        {
            context.StudentMeals.Add(studentMeal);
            context.SaveChanges();
        }

        // Check if a studentmeal already exists for a student and meal combination in database
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

        // Removes an existing studentmeal from database based on studentmeal
        public void DeleteStudentMeal(StudentMeal studentMeal)
        {
            context.StudentMeals.Remove(studentMeal);
            context.SaveChanges();
        }

        // Removes an existing studentmeal from database based studentId and mealId combination
        public void DeleteStudentMeal(int studentId, int mealId)
        {
            StudentMeal studentMeal = context.StudentMeals.Where(x => x.MealId == mealId && x.Student.StudentId == studentId).FirstOrDefault();
            context.StudentMeals.Remove(studentMeal);
            context.SaveChanges();
        }

        // Returns all students for a meal for studentmeals in database
        public IEnumerable<StudentMeal> GetStudentMealsForMeal(Meal meal)
        {
            IEnumerable<StudentMeal> studentMeals = context.StudentMeals.Where(x => x.MealId == meal.MealId);
            return studentMeals;
        }

        // Returns all students for all meals for student in database
        public IEnumerable<StudentMeal> GetStudentMealsForStudent(Student student)
        {
            IEnumerable<StudentMeal> studentMeals = context.StudentMeals.Where(x => x.StudentID == student.StudentId);
            return studentMeals;
        }
    }
}
