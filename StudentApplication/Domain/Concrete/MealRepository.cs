using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class MealRepository : IMealRepository
    {
        private EFDBContextStudent context = new EFDBContextStudent();

        // Add meal to database
        public void AddMeal(Meal meal)
        {
            context.Meals.Add(meal);
            context.SaveChanges();
        }

        // Remove meal from database
        public void DeleteMeal(Meal meal)
        {
            context.Meals.Remove(meal);
            context.SaveChanges();
        }

        // Get meal from database based on meal Id
        public Meal GetMeal(int mealId)
        {
            Meal meal = context.Meals.Where(x => x.MealId == mealId).FirstOrDefault();
            return meal;
        }

        // Gets all meals from database between two dates from database
        public IEnumerable<Meal> GetMeals(DateTime startDateTime, DateTime endDateTime)
        {
            IEnumerable<Meal> meals = context.Meals.Where(x => x.MealDateTime >= startDateTime && x.MealDateTime <= endDateTime);
            return meals;
        }

        // Updates existing meal in database
        public void UpdateMeal(Meal meal)
        {
            Meal dbEntry = context.Meals.Find(meal.MealId);
            context.Entry(dbEntry).CurrentValues.SetValues(meal);
            context.SaveChanges();
        }
    }
}
