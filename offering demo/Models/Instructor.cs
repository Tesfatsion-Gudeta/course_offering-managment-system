using System.ComponentModel.DataAnnotations;

namespace offering_demo.Models
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }
        public string ?Name { get; set; }
        public string? Gender { get; set; }
        public string? ARank { get; set; }
        public int DepartmentID { get; set; }

        public Department ?Department { get; set; }
    }
}
