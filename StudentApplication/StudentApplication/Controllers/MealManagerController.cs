using Domain;
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
        [Authorize(Roles = "Registered")]
        [HttpGet]
        public ActionResult CreateMeal(DateTime dateTime)
        {
            ViewBag.dateTime = dateTime.Date.ToShortDateString();
            TempData["DateTime"] = dateTime;
            return View();
        }

        [Authorize(Roles = "Registered")]
        [HttpPost]
        public ActionResult CreateMeal(Meal meal)
        {
            DateTime dateTime = (DateTime)TempData["DateTime"];
            if (!ModelState.IsValid)
            {
                ViewBag.dateTime = dateTime.Date.ToShortDateString();
                TempData["DateTime"] = dateTime;
                return View();
            }
            string userName = User.Identity.Name;
            Student student = studentRepository.GetStudent(userName);
            meal.CurrentGuests = 1;
            meal.StudentId = student.StudentId;
            meal.MealDateTime = dateTime;
            mealRepository.AddMeal(meal);
            StudentMeal studentMeal = new StudentMeal {MealId = meal.MealId, StudentID = student.StudentId, Cook = true };
            studentMealRepository.AddStudentMeal(studentMeal);
            return RedirectToAction("ViewMeals", "MealOverview");
        }

        public ActionResult MealInfo(int id)
        {
            Meal meal = mealRepository.GetMeal(id);
            List<StudentMeal> studentMeals = studentMealRepository.GetStudentMealsForMeal(meal).ToList();
            List<Student> students = new List<Student>();
            MealInfoModel mealInfoModel = new MealInfoModel { Meal = meal, StudentMeals = studentMeals};
            return View(mealInfoModel);
        }
    }
}