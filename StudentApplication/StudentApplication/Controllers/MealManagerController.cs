using Domain;
using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentApplication.Controllers
{
    public class MealManagerController : Controller
    {
        private IMealRepository mealRepository;
        private IStudentRepository studentRepository;

        public MealManagerController(IMealRepository mealRepository, IStudentRepository studentRepository)
        {
            this.mealRepository = mealRepository;
            this.studentRepository = studentRepository;
        }

        // GET: MealManager
        [HttpGet]
        public ActionResult CreateMeal(DateTime dateTime)
        {
            ViewBag.dateTime = dateTime;
            return View();
        }

        [HttpPost]
        public ActionResult CreateMeal(Meal meal, DateTime dateTime)
        {
            string userName = User.Identity.Name;
            Student student = studentRepository.GetStudent(userName);
            meal.CurrentGuests = 1;
            meal.Cook = student;
            meal.MealDateTime = dateTime;
            mealRepository.AddMeal(meal);
            return RedirectToAction("ViewMeals", "MealOverview");
        }
    }
}