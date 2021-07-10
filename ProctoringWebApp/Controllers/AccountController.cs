using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProctoringWebApp.Models;
using ProctoringWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProctoringWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public AccountController(
            UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager,
            ApplicationDbContext _dbContext
            )
        {
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._dbContext = _dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel data)
        {
            if (ModelState.IsValid)
            {
                Student student = _dbContext.Students.Where(x => x.RegistrationNumber.Equals(data.RegistrationNumber)).FirstOrDefault();

                if (student == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid credentials.");
                    return View(data);
                }

                IdentityUser user = await _userManager.FindByIdAsync(student.ApplicationUserId);
                var login = await _signInManager.PasswordSignInAsync(user.UserName, data.Password, true, false);
                if (login.Succeeded)
                {
                    HttpContext.Session.SetString("userId", user.Id);
                    //HttpContext.Session.SetString("userId", user.Id);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid credentials.");
                return View(data);
            }
            return View(data);
        }


        [HttpGet]
        [AllowAnonymous]

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterViewModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var query = _dbContext.Students.Where(x => x.RegistrationNumber.Equals(data.RegistrationNumber)).FirstOrDefault();
                    if (query != null)
                    {
                        ModelState.AddModelError(string.Empty, "Registration number already in use.");
                        return View(data);
                    }

                    var checkEmailExists = await _userManager.FindByEmailAsync(data.Email);
                    if (checkEmailExists != null)
                    {
                        ModelState.AddModelError(string.Empty, "Email address already in use.");
                        return View(data);
                    }
                    IdentityUser newUser = new IdentityUser()
                    {
                        UserName = data.FirstName,
                        Email = data.Email,
                        PasswordHash = data.Password
                    };
                    var user = await _userManager.CreateAsync(newUser, data.Password);
                    if (user.Succeeded)
                    {
                        Student newStd = new Student()
                        {
                            FirstName = data.FirstName,
                            LastName = data.LastName,
                            RegistrationNumber = data.RegistrationNumber,
                            ApplicationUserId = newUser.Id
                        };
                        _dbContext.Students.Add(newStd);
                        await _dbContext.SaveChangesAsync();
                        ViewBag.message = "Account created successfully.";
                        return View();
                    }
                }
                return View(data);
            }
            catch(Exception exc)
            {
                ViewBag.message = exc.Message;
                return View();
            }
        }
    }
}
