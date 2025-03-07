using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using offering_demo.data;
using offering_demo.Models;


namespace offering_demo.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly COMSDbContext _context;

        public DepartmentsController(COMSDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var DepartmentList = _context.Department.ToList();
            return View(DepartmentList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Department.Add(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _context.Department.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Department department)
        {
            if (id != department.DepartmentID)
            {
                return BadRequest();
            }

            var record = _context.Department.Find(id);
            if (record == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                record.DepartmentID = department.DepartmentID;
                record.DepartmentName = department.DepartmentName;
                record.DepartmentAcronym = department.DepartmentAcronym;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _context.Department.FirstOrDefault(d => d.DepartmentID == id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _context.Department.Find(id);

            if (department == null)
            {
                return NotFound();
            }

            _context.Department.Remove(department);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
