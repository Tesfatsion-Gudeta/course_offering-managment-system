using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using offering_demo.data;
using offering_demo.Models;


namespace offering_demo.Controllers
{
    public class CoursesController : Controller
    {
        private readonly COMSDbContext _context;

        public CoursesController(COMSDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var CourseList = _context.Course.Include(x => x.Department).ToList();
            return View(CourseList);
        }
        public IActionResult Create()
        {
            ViewBag.DepartmentList = new SelectList(_context.Department, "DepartmentID", "DepartmentName");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            _context.Course.Add(course); // Add course to the database
            _context.SaveChanges(); // Save changes
            return RedirectToAction("Index"); // Redirect to the list page after saving
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _context.Course.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            ViewBag.DepartmentList = new SelectList(_context.Department, "DepartmentID", "DepartmentName", course.DepartmentID);
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(int id, Course course)
        {
            var record = _context.Course.Find(id);

            if (record == null)
            {
                return NotFound();  // If course not found, return NotFound()
            }

            record.CourseName = course.CourseName;
            record.CourseCode = course.CourseCode;
            record.CreditHour = course.CreditHour;
            record.DepartmentID = course.DepartmentID;

            _context.SaveChanges(); // Save changes
            return RedirectToAction("Index"); // Redirect to the list page after saving
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _context.Course
                .Include(c => c.Department)
                .FirstOrDefault(m => m.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _context.Course.Find(id);

            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
