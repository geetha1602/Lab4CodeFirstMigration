using Lab4CodeFirstCrudApp;
using Microsoft.AspNetCore.Mvc;

namespace Lab4CodeFirstWebApp.Controllers
{
    public class DepartmentController : Controller
    {
        AppDbContext _db;
        public DepartmentController(AppDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            var result = _db.Departments.ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {
                var dep = new Department()
                {
                    DeptName = model.DeptName
                };
                _db.Departments.Add(dep);
                _db.SaveChanges();
                TempData["error"] = "Record Saved!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            var dep = _db.Departments.SingleOrDefault(e => e.DeptId == id);
            _db.Departments.Remove(dep);
            _db.SaveChanges();
            TempData["error"] = "Record Deleted!";
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var dep = _db.Departments.SingleOrDefault(e => e.DeptId == id);
            var result = new Department()
            {
                DeptId = dep.DeptId,
                DeptName = dep.DeptName
            };
            return View(result);
        }
        [HttpPost]
        public IActionResult Edit(Department model)
        {
            var dep = new Department()
            {
                DeptId=model.DeptId,
                DeptName = model.DeptName
            };
            _db.Departments.Update(dep);
            _db.SaveChanges();
            TempData["error"] = "Record Updated!";
            return RedirectToAction("Index");
        }

    } 
}
