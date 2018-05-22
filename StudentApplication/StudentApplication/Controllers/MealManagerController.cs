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
        private IStudentMealRepository studentMealRepository;

        public MealManagerController(IMealRepository mealRepository, IStudentRepository studentRepository, IStudentMealRepository studentMealRepository)
        {
            this.mealRepository = mealRepository;
            this.studentRepository = studentRepository;
            this.studentMealRepository = studentMealRepository;
        }

        // GET: MealManager
        [HttpGet]
        public ActionResult CreateMeal(DateTime dateTime)
        {
            ViewBag.dateTime = dateTime;
            TempData["DateTime"] = dateTime;
            return View();
        }

        [HttpPost]
        public ActionResult CreateMeal(Meal meal)
        {
            string userName = User.Identity.Name;
            Student student = studentRepository.GetStudent(userName);
            meal.CurrentGuests = 1;
            meal.Cook = student;
            meal.MealDateTime = (DateTime)TempData["DateTime"];
            mealRepository.AddMeal(meal);
            StudentMeal studentMeal = new StudentMeal {MealId = meal.MealId, Student = student, Cook = true };
            studentMealRepository.AddStudentMeal(studentMeal);
            return RedirectToAction("ViewMeals", "MealOverview");
        }
    }
}