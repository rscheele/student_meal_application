using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace StudentApplication.Models
{
    public class MealModel
    {
        public List<Meal> Meals { get; set; }
        public List<DateTime> Dates { get; set; }
        public List<StudentMeal> StudentMeals { get; set; }
        public Student Student { get; set; }
    }
}