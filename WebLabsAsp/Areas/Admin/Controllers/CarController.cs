using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLabsAsp.Data;
using WebLabsAsp.Entities;

namespace WebLabsAsp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<Car> _logger;

        public CarController(ApplicationDbContext context,
            IWebHostEnvironment hostEnvironment,
            ILogger<Car> logger)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        [BindProperty] public InputModel Input { get; set; }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            ViewData["groups"] = new SelectList(_context.CarGroups.ToList(), "CarGroupId", "GroupName");
            return View(await _context.Cars.ToListAsync());
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            ViewData["groups"] = new SelectList(_context.CarGroups.ToList(), "CarGroupId", "GroupName");
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Brand,Description,Price,Image,CarGroupId")]
            Car car)
        {
            if (ModelState.GetFieldValidationState("Brand") != ModelValidationState.Valid ||
                ModelState.GetFieldValidationState("Description") != ModelValidationState.Valid ||
                ModelState.GetFieldValidationState("Price") != ModelValidationState.Valid ||
                ModelState.GetFieldValidationState("CarGroupId") != ModelValidationState.Valid)
                return View(car);

            _context.Add(car);
            await _context.SaveChangesAsync();

            if (Input.ImageUpload == null) return RedirectToAction(nameof(Index));


            var image = car.Id + Path.GetExtension(Input.ImageUpload.FormFile.FileName);
            await using (var stream = new FileStream(Path.Combine(_hostEnvironment.WebRootPath, "cars",
                             image), FileMode.Create))
            {
                await Input.ImageUpload.FormFile.CopyToAsync(stream);
            }

            car.Image = image;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            ViewData["groups"] = new SelectList(_context.CarGroups.ToList(), "CarGroupId", "GroupName");
            return View(car);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Brand,Description,Price,Image,CarGroupId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.GetFieldValidationState("Brand") != ModelValidationState.Valid ||
                ModelState.GetFieldValidationState("Description") != ModelValidationState.Valid ||
                ModelState.GetFieldValidationState("Price") != ModelValidationState.Valid ||
                ModelState.GetFieldValidationState("CarGroupId") != ModelValidationState.Valid)
                return View(car);
            
            try
            {
                if (Input.ImageUpload != null)
                {
                    var fileName = car.Id.ToString() + '.'
                                                             + Input.ImageUpload.FormFile.FileName.Split('.')[1];
                    using (var stream = new FileStream(Path.Combine(_hostEnvironment.WebRootPath,
                               "cars", fileName), FileMode.Create))
                    {
                        await Input.ImageUpload.FormFile.CopyToAsync(stream);
                    }

                    car.Image = fileName;
                }
                else
                {
                    var fileName = car.Id.ToString();
                    var carImage = new DirectoryInfo(Path.Combine(_hostEnvironment.WebRootPath,
                        "cars")).GetFiles(fileName + "*.*");

                    car.Image = carImage.Length > 0 ? carImage[0].Name : "";
                }
    
                _context.Update(car);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(car.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
            }

            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();

            var fileInfo = _hostEnvironment.WebRootFileProvider.GetFileInfo("/cars/" + car.Image);
            if (!fileInfo.Exists) return RedirectToAction(nameof(Index));

            var oldPath = Path.Combine(_hostEnvironment.WebRootPath, "cars", car.Image);
            System.IO.File.Delete(oldPath);

            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(Guid id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }

        public class InputModel
        {
            [BindProperty] public FileUpload ImageUpload { get; set; }
        }
    }
}