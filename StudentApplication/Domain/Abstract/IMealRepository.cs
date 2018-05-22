using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IMealRepository
    {
        void AddMeal(Meal meal);
        Meal GetMeal(int mealId);
        IEnumerable<Meal> GetMeals(DateTime startDateTime, DateTime endDateTime);
        void DeleteMeal(Meal meal);
        void UpdateMeal(Meal meal);
    }
}
