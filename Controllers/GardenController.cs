using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using second.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace second.Controllers
{
    public class GardenController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;    
        public GardenController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            //login functionality will go here

            // find the user in the database
            var user = await _userManager.FindByNameAsync(username); 
            if (user != null) //if the user exist then continue
            {
                //sign in the user
                // sign in the user using signInManager service
                var LogResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (LogResult.Succeeded) //if the sign in succedes, redirect to other Index
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task <IActionResult> Register(string username, string password)
        {
            //register functionality will go here
            var user = new IdentityUser  //create identity object
            {
                UserName = username,
            };

            var result = await _userManager.CreateAsync(user, password); //create user with identity
            if (result.Succeeded) //if the creation succedes, the continue
            {
                //add user to the database here
                // sign in the user using sign in manager service
                var SignResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (SignResult.Succeeded) //if the sign in succedes then continue and redirect to home action method
                {
                    return RedirectToAction("Index");
                }

            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
