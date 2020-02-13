using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class AccountController : Controller
    {
        private readonly StoreContext _context;

        public AccountController(StoreContext context)
        {
            _context = context;
        }

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
    }
}