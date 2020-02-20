using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class AccountController : Controller
    {
        private readonly StoreContext _context;

        private readonly IHttpContextAccessor _http;

        public AccountController(StoreContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;

        }

        #region Registration Methods
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel reg)
        {
            if (ModelState.IsValid)
            {
                //Check Username is not taken
                if (!await AccountDb.IsUserNameTaken(reg.UserName, _context))
                {
                    Account acc = new Account()
                    {
                        FullName = reg.FullName,
                        Email = reg.Email,
                        Username = reg.UserName,
                        Password = reg.Password
                    };
                    //add account to database
                    await AccountDb.Register(acc, _context);

                    //create user session
                    SessionHelper.CreateUserSession( _http, acc.AccountID, acc.Username);
                    #region Manual CreateUserSession Practice
                    //HttpContext.Session.SetInt32("Id", acc.AccountID);
                    //HttpContext.Session.SetString("Username", acc.Username);
                    #endregion

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Display Error with other username error msg.
                    ModelState.AddModelError(nameof(Account.Username), "Username is already taken, Please pick another.");
                }
            }
            return View(reg);
        }
        #endregion

        #region Login Method
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                Account acc = await AccountDb.DoesUserMatch(login, _context);
                if (acc != null)
                {
                    SessionHelper.CreateUserSession(_http, acc.AccountID, acc.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Credintials");
                }
            }
            return View(login);
        }
        #endregion
        public IActionResult Logout()
        {
            SessionHelper.DestroyUserSession(_http);
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}