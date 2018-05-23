using Domain;
using Domain.Abstract;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentApplication.Controllers
{
    [Authorize(Roles = "Registration")]
    public class AccountStudentController : Controller
    {
        private IStudentRepository studentRepository;

        public AccountStudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }


        // GET: AccountStudent
        [HttpGet]
        public ActionResult StepTwo()
        {
            return View();
        }

        // Creates a student entity and changes the userrole from 'Registration' to 'Registered'
        [HttpPost]
        public ActionResult StepTwo(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            student.Email = User.Identity.Name;
            studentRepository.AddStudent(student);
            return RedirectToAction("AddRole", "Account");
        }
    }
}