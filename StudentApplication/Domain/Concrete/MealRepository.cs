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

        public void AddMeal(Meal meal)
        {
            throw new NotImplementedException();
        }

        public void DeleteMeal(Meal meal)
        {
            throw new NotImplementedException();
        }

        public Meal GetMeal(Meal meal)
        {
            throw new NotImplementedException();
        }

        public void UpdateMeal(Meal meal)
        {
            throw new NotImplementedException();
        }
    }
}
