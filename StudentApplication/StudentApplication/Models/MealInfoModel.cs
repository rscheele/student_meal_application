using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentApplication.Models
{
    public class MealInfoModel
    {
        public Meal Meal { get; set; }
        public List<StudentMeal> StudentMeals { get; set; }
    }
}