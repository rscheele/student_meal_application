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
            if (User.Identity.IsAuthenticated)
            {
                student = studentRepository.GetStudent(User.Identity.Name);
                studentMeals = studentMealRepository.GetStudentMealsForStudent(student).ToList();
            }
            MealModel mealModel = new MealModel { Meals = meals, Dates = dates, Student = student, StudentMeals = studentMeals};
            return View(mealModel);
        }

        [Authorize(Roles = "Registered")]
        public ActionResult JoinMeal(int id)
        {
            Meal meal = mealRepository.GetMeal(id);
            Student student = studentRepository.GetStudent(User.Identity.Name);
            bool exists = studentMealRepository.CheckForExisitingStudentMeal(student.StudentId, meal.MealId);
            if (exists == true)
            {
                return View("Exists");
            }
            if (meal.CurrentGuests >= meal.MaxGuests)
            {
                return View("Full");
            }
            meal.CurrentGuests = meal.CurrentGuests + 1;
            mealRepository.UpdateMeal(meal);
            StudentMeal studentMeal = new StudentMeal { MealId = meal.MealId, StudentID = student.StudentId, Cook = false };
            studentMealRepository.AddStudentMeal(studentMeal);
            return View("Joined");
        }

        [Authorize(Roles = "Registered")]
        public ActionResult ExitMeal(int id)
        {
            Meal meal = mealRepository.GetMeal(id);
            Student student = studentRepository.GetStudent(User.Identity.Name);
            bool exists = studentMealRepository.CheckForExisitingStudentMeal(student.StudentId, meal.MealId);
            if (exists != true)
            {
                return View("NoMeal");
            }
            meal.CurrentGuests = meal.CurrentGuests - 1;
            mealRepository.UpdateMeal(meal);
            studentMealRepository.DeleteStudentMeal(student.StudentId, meal.MealId);
            return View("Removed");
        }

        [Authorize(Roles = "Registered")]
        public ActionResult DeleteMeal(int id)
        {
            Meal meal = mealRepository.GetMeal(id);
            Student student = studentRepository.GetStudent(User.Identity.Name);
            if (meal.CurrentGuests >= 2)
            {
                return View("ExistingRegistrations");
            }
            StudentMeal studentMeal = studentMealRepository.GetStudentMealsForMeal(meal).FirstOrDefault();
            studentMealRepository.DeleteStudentMeal(studentMeal);
            mealRepository.DeleteMeal(meal);
            return View("Deleted");
        }

        [Authorize(Roles = "Registered")]
        public ActionResult EditMeal(int id)
        {
            Meal meal = mealRepository.GetMeal(id);
            Student student = studentRepository.GetStudent(User.Identity.Name);
            if (meal.CurrentGuests >= 2)
            {
                return View("ExistingRegistrations");
            }
            StudentMeal studentMeal = studentMealRepository.GetStudentMealsForMeal(meal).FirstOrDefault();
            studentMealRepository.DeleteStudentMeal(studentMeal);
            mealRepository.DeleteMeal(meal);
            return View("MealInfo");
        }
    }
}