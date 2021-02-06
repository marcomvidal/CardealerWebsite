using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cars.Infrastructure;
using Cars.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars.Controllers
{
    public class CarsController : Controller
    {
        private readonly Context _db;

        public CarsController(Context db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _db.Cars
                .OrderBy(x => x.Manufacturer)
                .ToListAsync();
            
            return View(cars);
        }

        public IActionResult Create()
        {
            return View("Form", new Car());
        }

        [HttpPost]
        public async Task<IActionResult> Save(Car car)
        {
            await _db.Cars.AddAsync(car);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var car = await _db.Cars.FindAsync(id);
            return View("Form", car);
        }

        public async Task<IActionResult> Update(Car car)
        {
            _db.Entry(await _db.Cars.FindAsync(car.Id)).CurrentValues.SetValues(car);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[controller]/{id:Guid}")]
        public async Task<ActionResult> Details(Guid id)
        {
            var car = await _db.Cars.FindAsync(id);
            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _db.Cars.FirstOrDefaultAsync(x => x.Id == id);
            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}