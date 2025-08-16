using Evaluation.Data;
using Evaluation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evaluation.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _iweb;


        public RegistrationController(ApplicationDbContext context, IWebHostEnvironment iweb)
        {
            _context = context;
            _iweb = iweb;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(IFormFile img_file, Registration obj)
        {


            var imgText = Path.GetExtension(img_file.FileName);
            if (imgText == ".jpg" || imgText == ".gif")
            {
                var uploading = Path.Combine(_iweb.WebRootPath, "images", img_file.FileName);

                var stream = new FileStream(uploading, FileMode.Create);
                await img_file.CopyToAsync(stream);
                stream.Close();

                obj.photo_name = img_file.FileName;
                obj.photo_path = uploading;
            }
            

            if (ModelState.IsValid)
            {
                _context.Registrations.Add(obj);
                await _context.SaveChangesAsync();

                TempData["message"] = "Registration successful!";
                return RedirectToAction("Register", "Registration");
            }

            TempData["message"] = "Registration Failed!";
            return View(obj);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModels r)
        {
            if (ModelState.IsValid)
            {
                var filtered = from l in _context.Registrations
                               where l.email == r.email && l.password == r.password
                               select l;

                if (filtered != null)
                {
                    foreach (var p in filtered)
                    {
                        HttpContext.Session.SetInt32("user_ID", p.user_id);
                        if (p.account_type == "Admin")
                        {
                            return new RedirectResult(url: "/Admin/Index", permanent: true, preserveMethod: true);
                        }
                        else
                        {                          
                            return new RedirectResult(url: "/Staff/Index", permanent: true, preserveMethod: true);
                        }
                    }
                }

                return View();
            }
            return View();
        }
    }
}
