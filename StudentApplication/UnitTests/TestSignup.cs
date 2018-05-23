using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentApplication.Controllers;

namespace UnitTests
{
    [TestClass]
    public class TestSignup
    {
        [TestMethod]
        public void TestJoinMeal()
        {
            // Create the data (students, meal and studentmeals)
            Student student1 = new Student { StudentId = 1, Name = "Mark", Email = "test1@gmail.com", Phone = "0123456" };
            Student student2 = new Student { StudentId = 2, Name = "Fred", Email = "test2@gmail.com", Phone = "4538737" };
            Student student3 = new Student { StudentId = 3, Name = "Iris", Email = "test3@gmail.com", Phone = "7534714" };
            Student student4 = new Student { StudentId = 4, Name = "Daan", Email = "test4@gmail.com", Phone = "7820784" };
            Meal meal1 = new Meal { MealId = 1, StudentId = 1, CurrentGuests = 3, MaxGuests = 3, Name = "Spahget", Description = "Lekker omas spaghet", MealDateTime = DateTime.Parse("2018-05-24 00:00:00.000"), Price = Decimal.Parse("3,50") };
            Meal meal2 = new Meal { MealId = 2, StudentId = 1, CurrentGuests = 3, MaxGuests = 4, Name = "Aardappels", Description = "Net zoals ze in Soviet Rusland aten", MealDateTime = DateTime.Parse("2018-05-24 00:00:00.000"), Price = Decimal.Parse("0,50") };

            // Create and set up the mock repositories
            Mock<IStudentRepository> mockStudentRepository = new Mock<IStudentRepository>();
            mockStudentRepository.Setup(m => m.GetStudent("test4@gmail.com")).Returns(student4);
            Mock<IMealRepository> mockMealRepository = new Mock<IMealRepository>();
            mockMealRepository.Setup(m => m.GetMeal(1)).Returns(meal1);
            mockMealRepository.Setup(m => m.GetMeal(2)).Returns(meal2);
            Mock<IStudentMealRepository> mockStudentMealRepository = new Mock<IStudentMealRepository>();

            // Setup fake identity
            var mocks = new MockRepository(MockBehavior.Default);
            Mock<IPrincipal> mockPrincipal = mocks.Create<IPrincipal>();
            mockPrincipal.SetupGet(p => p.Identity.Name).Returns("test4@gmail.com");
            mockPrincipal.Setup(p => p.IsInRole("Registered")).Returns(true);

            // Create mock controller context for authentication
            var mockContext = new Mock<ControllerContext>();
            mockContext.SetupGet(p => p.HttpContext.User).Returns(mockPrincipal.Object);
            mockContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            // Setup the controller including fake login
            MealOverviewController mealOverviewController = new MealOverviewController(mockMealRepository.Object, mockStudentRepository.Object, mockStudentMealRepository.Object) { ControllerContext = mockContext.Object };

            // Check if the view returned is the view that should be returned when a meal is fully booked
            var viewResult = mealOverviewController.JoinMeal(1) as ViewResult;
            Assert.IsTrue(viewResult.ViewName == "Full");

            // Check if currentguests has not been updated and is still 3 and not 4 in the mocked repository
            Assert.AreEqual(mockMealRepository.Object.GetMeal(1).CurrentGuests, 3);
            Assert.AreNotEqual(mockMealRepository.Object.GetMeal(1).CurrentGuests, 4);

            // Now check what happens when the meal isn't fully booked
            var viewResult2 = mealOverviewController.JoinMeal(2) as ViewResult;
            Assert.IsTrue(viewResult2.ViewName == "Joined");

            // See if the total guests have been updated from 3 to 4 in the mocked repository
            Assert.AreNotEqual(mockMealRepository.Object.GetMeal(2).CurrentGuests, 3);
            Assert.AreEqual(mockMealRepository.Object.GetMeal(2).CurrentGuests, 4);
        }
    }
}
