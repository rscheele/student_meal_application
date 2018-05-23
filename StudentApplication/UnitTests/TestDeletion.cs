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
    public class TestDeletion
    {
        [TestMethod]
        public void TestDeleteMeal()
        {
            // Create the data (students, meal and studentmeals)
            Meal meal1 = new Meal { MealId = 1, StudentId = 4, Cook = new Student { StudentId = 4, Name = "Daan", Email = "test4@gmail.com", Phone = "7820784" }, CurrentGuests = 1, MaxGuests = 3, Name = "Spahget", Description = "Lekker omas spaghet", MealDateTime = DateTime.Parse("2018-05-24 00:00:00.000"), Price = Decimal.Parse("3,50") };
            Meal meal2 = new Meal { MealId = 2, StudentId = 4, Cook = new Student { StudentId = 4, Name = "Daan", Email = "test4@gmail.com", Phone = "7820784" }, CurrentGuests = 2, MaxGuests = 4, Name = "Aardappels", Description = "Net zoals ze in Soviet Rusland aten", MealDateTime = DateTime.Parse("2018-05-24 00:00:00.000"), Price = Decimal.Parse("0,50") };
            Student student = new Student { StudentId = 4, Name = "Daan", Email = "test4@gmail.com", Phone = "7820784" };

            // Create and set up the mock repositories
            Mock<IStudentRepository> mockStudentRepository = new Mock<IStudentRepository>();
            mockStudentRepository.Setup(m => m.GetStudent("test4@gmail.com")).Returns(student);
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
            MealManagerController mealManagerController = new MealManagerController(mockMealRepository.Object, mockStudentRepository.Object, mockStudentMealRepository.Object) { ControllerContext = mockContext.Object };
                        
            // Check if the view returned is the view that should be returned when an meal with no bookings is deleted
            var viewResult = mealManagerController.DeleteMeal(1) as ViewResult;
            Assert.IsTrue(viewResult.ViewName == "Deleted");

            // Now check what happens when the meal is booked by someone and if it returns the correct view
            var viewResult2 = mealManagerController.DeleteMeal(2) as ViewResult;
            Assert.IsTrue(viewResult2.ViewName == "ExistingRegistrations");

            // See if the meal is still in the repository
            Assert.AreEqual(mockMealRepository.Object.GetMeal(2), meal2);
        }
    }
}
