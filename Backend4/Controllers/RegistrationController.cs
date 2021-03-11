using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend4.Models;
using Backend4.Models.Controls;
using Backend4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend4.Controllers
{
    public class RegistrationController : Controller
    {
        private IRegistrationService registrationService;
        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        public IActionResult Index()
        {
            var model = new RegistrationPrimaryDataViewModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(RegistrationPrimaryDataViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if(registrationService.CheckUser(model.FirstName,
                                             model.LastName,
                                             model.Day,
                                             model.Month,
                                             model.Year,
                                             model.Gender))
            {
                model.UserExisted = true;
                return this.View("SignUpAlreadyExists", model);
            }

            return this.View("SignUpCredentials", new RegistrationSecondaryDataViewModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Day = model.Day,
                Month = model.Month,
                Year = model.Year,
                Gender = model.Gender,
                UserExisted = false
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpAlreadyExists(RegistrationPrimaryDataViewModel model)
        {
            
            return this.View("SignUpCredentials", new RegistrationSecondaryDataViewModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Day = model.Day,
                Month = model.Month,
                Year = model.Year,
                Gender = model.Gender,
                UserExisted = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUpCredentials(RegistrationSecondaryDataViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            /*if (!this.registrationService.VerifyPassword(model.Password, model.ConfirmPassword))
            {
                this.ModelState.AddModelError("Password", "Password mismatch");
                return this.View(model);
            }*/

            registrationService.RegistrationUser(model.FirstName,
                                                 model.LastName,
                                                 model.Day,
                                                 model.Month,
                                                 model.Year,
                                                 model.Gender,
                                                 model.Email,
                                                 model.Password,
                                                 model.Remembered);

            return this.View("SignUpResult", new RegistrationSecondaryDataViewModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Day = model.Day,
                Month = model.Month,
                Year = model.Year,
                Gender = model.Gender,
                UserExisted = model.UserExisted,
                Remembered = model.Remembered,
                Password = model.Password,
                Email = model.Email,
            });
        }
    }
}
