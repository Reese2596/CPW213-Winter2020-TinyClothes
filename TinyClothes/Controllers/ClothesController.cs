using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyClothes.Data;

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
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}