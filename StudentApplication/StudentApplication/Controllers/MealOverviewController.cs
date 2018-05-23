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
            DateTime twoWeeks = DateTime.Today.AddDays(14);
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

        public ActionResult JoinMeal(int id)
        {
            Meal meal = mealRepository.GetMeal(id);
            Student student = studentRepository.GetStudent(User.Identity.Name);
            bool exists = studentMealRepository.CheckForExisitingStudentMeal(student.StudentId, meal.MealId);
            if (exists == true)
            {
                return View();
            }
            return View();
        }
    }
}