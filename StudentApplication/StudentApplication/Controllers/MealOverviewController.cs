using Domain.Abstract;
using Domain.Entities;
using StudentApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentApplication.Controllers
{
    public class MealOverviewController : Controller
    {
        private IMealRepository mealRepository;
        private IStudentRepository studentRepository;

        public MealOverviewController(IMealRepository mealRepository, IStudentRepository studentRepository)
        {
            this.mealRepository = mealRepository;
            this.studentRepository = studentRepository;
        }

        // GET: MealOverview
        public ActionResult ViewMeals()
        {
            DateTime now = DateTime.Now.Date;
            DateTime twoWeeks = DateTime.Today.AddDays(14);
            List<Meal> meals = mealRepository.GetMeals(now, twoWeeks).ToList();
            List<DateTime> dates = new List<DateTime>();
            for (var dt = now; dt <= twoWeeks; dt = dt.AddDays(1))
                dates.Add(dt);
            MealModel mealModel = new MealModel { Meals = meals, Dates = dates };
            return View(mealModel);
        }
    }
}