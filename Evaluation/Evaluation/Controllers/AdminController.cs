using Evaluation.Data;
using Evaluation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Evaluation.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Logout()
        {
            return new RedirectResult(url: "/Registration/Login", permanent: true, preserveMethod: true);
        }

        public IActionResult Index()
        {
            var objRegistrations = _context.Registrations.ToList();

            return View(objRegistrations);
        }

        public async Task<IActionResult> AssignSalary(int user_id)
        {
            if (user_id == 0)
            {
                return NotFound();
            }

            var joinedData = await (from t1 in _context.Registrations
                                    where t1.user_id == user_id
                                    select new SalaryViewModel
                                    {
                                        user_id = t1.user_id,
                                        name = t1.name,
                                        dob = t1.dob,
                                        gender = t1.gender,
                                        account_type = t1.account_type,
                                        email = t1.email,
                                        password = t1.password,
                                        photo_name = t1.photo_name,
                                        photo_path = t1.photo_path,

                                        // salary fields left empty (to be assigned later)
                                        salary_id = 0,
                                        month = "",
                                        year = 0,
                                        salary = 0
                                    }).FirstOrDefaultAsync();


            if (joinedData == null)
            {
                return NotFound();
            }

            return View(joinedData); 
        }

        [HttpPost]
        public async Task<IActionResult> SalaryAllocation(SalaryViewModel obj)
        {
            var salary = new Salary
            {
                user_id = obj.user_id,
                month = obj.month,
                year = obj.year,
                salary = obj.salary
            };

            _context.salaries.Add(salary);
            await _context.SaveChangesAsync();

            TempData["message"] = "Salary allocated successfully!";
            return RedirectToAction("AssignSalary", new { user_id = obj.user_id });
        }

        public IActionResult Profile()
        {
            int? id = HttpContext.Session.GetInt32("user_ID");
            var user = _context.Registrations.Find(id);
            return View(user);
        }
    }
}
