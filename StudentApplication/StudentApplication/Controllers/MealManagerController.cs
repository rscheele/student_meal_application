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

        // Shows the meal creation form
        [Authorize(Roles = "Registered")]
        [HttpGet]
        public ActionResult CreateMeal(DateTime dateTime)
        {
            ViewBag.dateTime = dateTime.Date.ToShortDateString();
            TempData["DateTime"] = dateTime;
            return View();
        }

        // Post the meal creation form
        [Authorize(Roles = "Registered")]
        [HttpPost]
        public ActionResult CreateMeal(Meal meal)
        {
            DateTime dateTime = (DateTime)TempData["DateTime"];
            // Check for valid modelstate otherwise return the form
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

        // Show meal info
        [HttpGet]
        public ActionResult MealInfo(int id)
        {
            Meal meal = mealRepository.GetMeal(id);
            List<StudentMeal> studentMeals = studentMealRepository.GetStudentMealsForMeal(meal).ToList();
            List<Student> students = new List<Student>();
            MealInfoModel mealInfoModel = new MealInfoModel { Meal = meal, StudentMeals = studentMeals};
            return View(mealInfoModel);
        }

        // Shows the form for editing a meal
        [Authorize(Roles = "Registered")]
        [HttpGet]
        public ActionResult EditMeal(int id)
        {
            // Check if the person trying to edit it is the cook, if not it will shows noaccess
            Meal meal = mealRepository.GetMeal(id);
            if (User.Identity.Name != meal.Cook.Email)
            {
                return View("NoAccess");
            }
            // If people have signed up for your meal you can't edit it anymore
            if (meal.CurrentGuests >= 2)
            {
                return View("ExistingRegistrations");
            }
            TempData["MealId"] = id;
            return View(meal);
        }

        // Posts the form for editing a meal
        [Authorize(Roles = "Registered")]
        [HttpPost]
        public ActionResult EditMeal(Meal meal)
        {
            int id = (int)TempData["MealId"];
            Meal originalMeal = mealRepository.GetMeal(id);
            // Check if the modelstate is valid otherwise return the form
            if (!ModelState.IsValid)
            {
                TempData["MealId"] = id;
                return View(originalMeal);
            }
            originalMeal.Name = meal.Name;
            originalMeal.Price = meal.Price;
            originalMeal.MaxGuests = meal.MaxGuests;
            originalMeal.Description = meal.Description;
            mealRepository.UpdateMeal(originalMeal);
            return View("Success");
        }

        // Deletes an existing meal
        [Authorize(Roles = "Registered")]
        public ActionResult DeleteMeal(int id)
        {
            Meal meal = mealRepository.GetMeal(id);
            // Check if the person trying to edit it is the cook, if not it will shows noaccess
            if (User.Identity.Name != meal.Cook.Email)
            {
                return View("NoAccess");
            }
            Student student = studentRepository.GetStudent(User.Identity.Name);
            // If people have signed up for your meal you can't edit it anymore
            if (meal.CurrentGuests >= 2)
            {
                return View("ExistingRegistrations");
            }
            StudentMeal studentMeal = studentMealRepository.GetStudentMealsForMeal(meal).FirstOrDefault();
            studentMealRepository.DeleteStudentMeal(studentMeal);
            mealRepository.DeleteMeal(meal);
            return View("Deleted");
        }
    }
}