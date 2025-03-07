using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using offering_demo.data;
using offering_demo.Models;


namespace offering_demo.Controllers
{
    public class SemestersController : Controller
    {
        private readonly COMSDbContext _context;

        public SemestersController(COMSDbContext context)
        {
            _context = context;
        }


  
        public IActionResult Index()
        {
            var CourseList = _context.Semester.ToList();
            return View(CourseList);
        }
        public IActionResult Create()
        {
            ViewBag.SemestersList = new SelectList(_context.Semester, "SemesterId", "SemesterName", "SemesterNo");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Semester semester)
        {
            _context.Semester.Add(semester); // Add course to the database
            _context.SaveChanges(); // Save changes
            return RedirectToAction("Index"); // Redirect to the list page after saving
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = _context.Semester.Find(id);
            if (semester == null)
            {
                return NotFound();
            }

            ViewBag.DepartmentList = new SelectList(_context.Department, "DepartmentID", "DepartmentName", semester.SemesterId);
            return View(semester);
        }

        [HttpPost]
        public IActionResult Edit(int id, Semester semester)
        {
            var record = _context.Semester.Find(id);

            if (record == null)
            {
                return NotFound();  // If course not found, return NotFound()
            }

            record.SemesterId = semester.SemesterId;
            record.SemesterName = semester.SemesterName;
            record.SemesterNo = semester.SemesterNo;
           

            _context.SaveChanges(); // Save changes
            return RedirectToAction("Index"); // Redirect to the list page after saving
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = _context.Semester.FirstOrDefault(s => s.SemesterId == id);

            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var semester = _context.Semester.Find(id);

            if (semester == null)
            {
                return NotFound();
            }

            _context.Semester.Remove(semester);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
