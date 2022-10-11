using Lab4CodeFirstCrudApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Lab4CodeFirstWebApp.Models;
using Microsoft.Extensions.Hosting;

namespace Lab4CodeFirstWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(AppDbContext db, IWebHostEnvironment env)
        {
            this._db = db;
            _env = env;
        }

       
        public IActionResult Index()
        {
            var employees = _db.Employees.Include(e => e.Department);
            return View(employees.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Department = new SelectList(_db.Departments, "DeptId", "DeptName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                var dep = new Employee()
                {
                    EmpName = model.EmpName,
                    Address = model.Address,
                    Salary = model.Salary,
                    ImagePath = uniqueFileName,
                    DepartmentId = model.DepartmentId
                };
                ViewBag.Department = new SelectList(_db.Departments, "DeptId", "DeptName", model.Department);
                _db.Employees.Add(dep);
                _db.SaveChanges();
                TempData["error"] = "Record Saved!";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private string ProcessUploadedFile(EmployeeViewModel model)
        {
            string uniqueFileName = null;
            string path = Path.Combine(_env.WebRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (model.UploadImage != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.UploadImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.UploadImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public IActionResult Delete(int id)
        {
            var dep = _db.Employees.SingleOrDefault(e => e.EmpId == id);
            string deleteFileFromFolder = Path.Combine(_env.WebRootPath, "Uploads");
            var CurrentImage = Path.Combine(Directory.GetCurrentDirectory(), deleteFileFromFolder, dep.ImagePath);
            _db.Employees.Remove(dep);
            if (System.IO.File.Exists(CurrentImage))
            {
                System.IO.File.Delete(CurrentImage);
            }
            _db.SaveChanges();
            TempData["error"] = "Record Deleted!";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var dep = _db.Employees.SingleOrDefault(e => e.EmpId == id);
            string name = Path.GetFileName(dep.ImagePath);

            var result = new EmployeeViewModel()
            {
                EmpId = dep.EmpId,
                EmpName = dep.EmpName,
                Address = dep.Address,
                Salary = dep.Salary,
                ExistingImage = dep.ImagePath,
                DepartmentId=dep.DepartmentId
            };
            ViewBag.Department = new SelectList(_db.Departments, "DeptId", "DeptName", dep.Department);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeViewModel model)
        {
            var mm =  _db.Employees.Find(model.EmpId);
            mm.EmpName = model.EmpName;
            mm.Address = model.Address;
            mm.Salary = model.Salary;
            mm.DepartmentId = model.DepartmentId;

            if (model.UploadImage != null)
            {
                if (model.ExistingImage != null)
                {
                    string filePath = Path.Combine(_env.WebRootPath, "Uploads", model.ExistingImage);
                    System.IO.File.Delete(filePath);
                }

                mm.ImagePath = ProcessUploadedFile(model);
            }
            ViewBag.Department = new SelectList(_db.Departments, "DeptId", "DeptName", model.Department);
            _db.Employees.Update(mm);
            _db.SaveChanges();
            TempData["error"] = "Record Updated!";
            return RedirectToAction("Index");
        }

    }
}
