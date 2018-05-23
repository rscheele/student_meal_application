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
    public class MealOverviewController : Controller
    {
        private IMealRepository mealRepository;
        private IStudentRepository studentRepository;
        private IStudentMealRepository studentMealRepository;

        public MealOverviewController(IMealRepository mealRepository, IStudentRepository studentRepository, IStudentMealRepository studentMealRepository)
        {
            this.mealRepository = mealRepository;
            this.studentRepository = studentRepository;
            this.studentMealRepository = studentMealRepository;
        }

        // GET: MealOverview
        // Show a table with the current meals
        public ActionResult ViewMeals()
        {
            DateTime now = DateTime.Now.Date;
            DateTime twoWeeks = DateTime.Today.AddDays(13);
            List<Meal> meals = mealRepository.GetMeals(now, twoWeeks).ToList();
            List<DateTime> dates = new List<DateTime>();
            for (var dt = now; dt <= twoWeeks; dt = dt.AddDays(1))
                dates.Add(dt);
            Student student = new Student();
            List<StudentMeal> studentMeals = new List<StudentMeal>();
            if (User.IsInRole("Registered"))
            {
                student = studentRepository.GetStudent(User.Identity.Name);
                studentMeals = studentMealRepository.GetStudentMealsForStudent(student).ToList();
            }
            MealModel mealModel = new MealModel { Meals = meals, Dates = dates, Student = student, StudentMeals = studentMeals};
            return View(mealModel);
        }

        // Join a created meal
        [Authorize(Roles = "Registered")]
        public ActionResult JoinMeal(int id)
        {
            Meal meal = mealRepository.GetMeal(id);
            Student student = studentRepository.GetStudent(User.Identity.Name);
            bool exists = studentMealRepository.CheckForExisitingStudentMeal(student.StudentId, meal.MealId);
            // Check if a student is already assigned to a meal, if so, you can't add another signup
            if (exists == true)
            {
                return View("Exists");
            }
            // Check if a meal is at capacity
            if (meal.CurrentGuests >= meal.MaxGuests)
            {
                return View("Full");
            }
            // If none of these conditions apply the student is added to the meal
            meal.CurrentGuests = meal.CurrentGuests + 1;
            mealRepository.UpdateMeal(meal);
            StudentMeal studentMeal = new StudentMeal { MealId = meal.MealId, StudentID = student.StudentId, Cook = false };
            studentMealRepository.AddStudentMeal(studentMeal);
            return View("Joined");
        }

        // Remove a registration for a heal
        [Authorize(Roles = "Registered")]
        public ActionResult ExitMeal(int id)
        {
            Meal meal = mealRepository.GetMeal(id);
            Student student = studentRepository.GetStudent(User.Identity.Name);
            bool exists = studentMealRepository.CheckForExisitingStudentMeal(student.StudentId, meal.MealId);
            // Check if you are actually registered to the meal
            if (exists != true)
            {
                return View("NoMeal");
            }
            meal.CurrentGuests = meal.CurrentGuests - 1;
            mealRepository.UpdateMeal(meal);
            studentMealRepository.DeleteStudentMeal(student.StudentId, meal.MealId);
            return View("Removed");
        }
    }
}