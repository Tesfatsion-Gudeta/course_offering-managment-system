using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using offering_demo.Models;

namespace offering_demo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Define struct correctly
    public struct Student
    {
        public string Name;
        public string Gender;

     
    }
    public IActionResult Index()
    {
        List<Student> students = new List<Student>();

        Student s1 = new Student();
        Student s2 = new Student();

        // Assign a name to the student
        s1.Name = "TesfaG";
        s1.Gender = "Male";

        s2.Name = "John doe";
        s2.Gender = "Male";

        students.Add(s1);
        students.Add(s2);

        ViewBag.students = students;
        ViewBag.listSize = students.Count;

        ViewBag.Message = "First ASP.Net Core program";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
