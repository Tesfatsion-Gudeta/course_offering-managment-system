using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using offering_demo.data;
using offering_demo.Models;


namespace offering_demo.Controllers
{
    public class OfferingsController : Controller
    {
        private readonly COMSDbContext _context;

        public OfferingsController(COMSDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var OfferingList = _context.Offering.Include(x => x.Course).ThenInclude(x => x.Department).Include(x => x.Instructor).ToList();
            return View(OfferingList);
        }
        public IActionResult Create()
        {
             ViewBag.DepartmentList = new SelectList(_context.Department, "DepartmentID", "DepartmentName");
            ViewBag.InstructorList = new SelectList(_context.Instructor, "InstructorId", "Name");
            ViewBag.CoursesList = new SelectList(_context.Course,"CourseId","CourseName");
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Create(Offering offering)
        {
            if (ModelState.IsValid)
            {
                // Fetch related entities (Course and Instructor) based on the ids
                offering.Course = _context.Course.FirstOrDefault(c => c.CourseId == offering.CourseId);
                offering.Instructor = _context.Instructor.FirstOrDefault(i => i.InstructorId == offering.InstructorId);

                // Check if related entities were found
                if (offering.Course == null || offering.Instructor == null)
                {
                    // Handle the case where the course or instructor doesn't exist
                    ModelState.AddModelError(string.Empty, "Invalid Course or Instructor.");
                    return View(offering);
                }

                // Add the offering entity to the context and save
                _context.Offering.Add(offering);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(offering);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offering = _context.Offering.Find(id);
            if (offering == null)
            {
                return NotFound();
            }
           ViewBag.DepartmentList = new SelectList(_context.Department, "DepartmentID", "DepartmentName");
            ViewBag.InstructorList = new SelectList(_context.Instructor, "InstructorId", "Name");
            ViewBag.CoursesList = new SelectList(_context.Course,"CourseId","CourseName");
            return View(offering);
        }

        //[HttpPost]
        //public IActionResult Edit(int id, Offering offering)
        //{
        //    var record = _context.Offering.Find(id);

        //    if (record == null)
        //    {
        //        return NotFound();  // If course not found, return NotFound()
        //    }

        //    record.Course.CourseName = offering.Course.CourseName;
        //    record.Course.CourseCode = offering.Course.CourseCode;
        //    record.Course.CreditHour = offering.Course.CreditHour;
        //    record.Instructor.Name = offering.Instructor.Name;

        //    _context.SaveChanges(); // Save changes
        //    return RedirectToAction("Index"); // Redirect to the list page after saving
        //}

        [HttpPost]
        public IActionResult Edit(int id, Offering offering)
        {
            if (id != offering.OfferingID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var existingOffering = _context.Offering
                    .Include(o => o.Course)
                    .Include(o => o.Instructor)
                    .FirstOrDefault(o => o.OfferingID == id);

                if (existingOffering == null)
                {
                    return NotFound();
                }

                // Update fields
                existingOffering.CourseId = offering.CourseId;
                existingOffering.InstructorId = offering.InstructorId;

                // Fetch and update related entities
                existingOffering.Course = _context.Course.FirstOrDefault(c => c.CourseId == offering.CourseId);
                existingOffering.Instructor = _context.Instructor.FirstOrDefault(i => i.InstructorId == offering.InstructorId);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate ViewBag in case of validation failure
            ViewBag.DepartmentList = new SelectList(_context.Department, "DepartmentID", "DepartmentName");
            ViewBag.InstructorList = new SelectList(_context.Instructor, "InstructorId", "Name");
            ViewBag.CoursesList = new SelectList(_context.Course, "CourseId", "CourseName");

            return View(offering);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offering = _context.Offering
                .Include(o => o.Course)
                .Include(o => o.Instructor)
                .FirstOrDefault(m => m.OfferingID == id);

            if (offering == null)
            {
                return NotFound();
            }

            return View(offering);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var offering = _context.Offering.Find(id);

            if (offering == null)
            {
                return NotFound();
            }

            _context.Offering.Remove(offering);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
