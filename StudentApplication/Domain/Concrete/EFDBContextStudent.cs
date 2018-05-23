using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    // Database context
    public class EFDBContextStudent : DbContext
    {
        public EFDBContextStudent() : base("EFDBContextStudent")
        {

        }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentMeal> StudentMeals { get; set; }
    }
}
