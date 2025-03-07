using System.ComponentModel.DataAnnotations;

namespace offering_demo.Models
{
    public class Semester
    {
        [Key]
        public int SemesterId { get; set; }
        public string SemesterName { get; set; } = "";
        public int SemesterNo { get; set; }

        public ICollection<Course> ?Course { get; set; }  // Used as FK for Course Table
    }
}
