using static offering_demo.Controllers.CoursesController;
using static offering_demo.Controllers.InstructorsController;
using System.ComponentModel.DataAnnotations;

namespace offering_demo.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string ?DepartmentName { get; set; }
        public string ?DepartmentAcronym { get; set; }

        public ICollection<Course> ?Courses { get; set; } // Used as FK for Course Table
        public ICollection<Instructor> ?Instructors { get; set; } // Used as FK for Course Table
    }
}
