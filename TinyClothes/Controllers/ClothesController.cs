using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;
using TinyClothes.Models;

namespace TinyClothes.Controllers
{
    public class ClothesController : Controller
    {
        //read only is good for not allowing modification unless its the constructor
        //no reassignment 
        private readonly StoreContext _context;

        public ClothesController(StoreContext context)
        {
            _context = context; //or this._context = context;
        }

        [HttpGet]
        public IActionResult ShowAll()
        {
            //Just a placeholder til we fix it
            IEnumerable<Clothing> clothes = new List<Clothing>();
            return View(clothes);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Clothing c)
        {
            if (ModelState.IsValid)
            {
                await ClothingDb.Add(_context, c);
                //Temp data last for one redirect
                TempData["Msg"] = $"{c.Title} added successfully!";
                return RedirectToAction("ShowAll");
            }
            //Return same view with validation msgs.
            return View(c);
        }
    }
}