using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> ShowAll(int? page)
        {
            const int pageSize = 2;

            //null coalescing operator
            //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
            int pageNumber = page ?? 1; //(page.HasValue)? page.Value : 1;
            ViewData["CurrPage"] = pageNumber;

            int maxPage = await GetMaxPage(pageSize);
            ViewData["MaxPage"] = maxPage;

            IEnumerable<Clothing> clothes = await ClothingDb.GetClothingByPage(_context,
                pageNum: pageNumber, pageSize: pageSize);

            return View(clothes);
        }

        private async Task<int> GetMaxPage(int pageSize)
        {
            int NumProducts = await ClothingDb.GetNumClothing(_context);
            //num product and page size are both an int so to get the decimal you must convert to a double
            // than to round up you must use Math.Ceiling which is a double so you must convert back to int
            int maxPage = Convert.ToInt32
                            (Math.Ceiling((double)NumProducts / pageSize));
            return maxPage;
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Clothing c = 
                await ClothingDb.GetClothingByID(id, _context);
            if(c == null)       //Clothing Not in the Db
            {
                //returns a Http 404 - Not Found 
                return NotFound();
            }
            return View(c);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Clothing c)
        {
            if (ModelState.IsValid)
            {
                await ClothingDb.Edit(c, _context);
                //tempData last for one redirect
                ViewData["Msg"] = c.Title + " Updated Successfully!"; // <- Same thing, ViewBag
            }
            return View(c);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Clothing c = await ClothingDb.GetClothingByID(id, _context);
            //If id does not exist, go to 404 page
            if(c == null)   
            {
                return NotFound();
            }
            return View(c);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Clothing c = await ClothingDb.GetClothingByID(id, _context);
            await ClothingDb.Delete(c, _context);
            TempData["Msg"] = $"{c.Title} Deleted Successfully.";
            return RedirectToAction(nameof(ShowAll));
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchCriteria search)
        {
            //Prepare query: Select * from Clothing.
            //Does not get sent to DB.
            if (ModelState.IsValid)
            {
                if (search.IsBeingSearched())
                {
                    await ClothingDb.BuildSearchQuery(search, _context);
                    return View(search);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "You must search by atleast one criteria.");
                    return View(search);
                }
            }
            return View();
        }
    }
}