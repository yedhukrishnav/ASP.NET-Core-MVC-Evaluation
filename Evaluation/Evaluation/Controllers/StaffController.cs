using Evaluation.Data;
using Evaluation.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Evaluation.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _iweb;


        public StaffController(ApplicationDbContext context, IWebHostEnvironment iweb)
        {
            _context = context;
            _iweb = iweb;
        }
        public IActionResult Logout()
        {
            return new RedirectResult(url: "/Registration/Login", permanent: true, preserveMethod: true);
        }

        public IActionResult Index()
        {
            int? id = HttpContext.Session.GetInt32("user_ID");
            var user = _context.Registrations.Find(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(IFormFile img_file, Registration obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            var existingUser = await _context.Registrations.FindAsync(obj.user_id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.name = obj.name;
            existingUser.dob = obj.dob;
            existingUser.gender = obj.gender;
            existingUser.account_type = obj.account_type;
            existingUser.email = obj.email;
            existingUser.password = obj.password;

            if (img_file != null && img_file.Length > 0)
            {
                var extension = Path.GetExtension(img_file.FileName).ToLower();
                if (extension == ".jpg" || extension == ".gif" || extension == ".png")
                {
                    var uploadPath = Path.Combine(_iweb.WebRootPath, "images", img_file.FileName);

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await img_file.CopyToAsync(stream);
                    }

                    existingUser.photo_name = img_file.FileName;
                    existingUser.photo_path = uploadPath;
                }
            }

            _context.Update(existingUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Report()
        {
            int? id = HttpContext.Session.GetInt32("user_ID");

            var salaries = _context.salaries
                                   .Where(s => s.user_id == id)
                                   .ToList();

            return View(salaries);  
        }

    }
}
