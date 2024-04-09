using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fancy.Web.ViewModels;
using Fancy.Core.Services;
using Fancy.Core.Entities;
using System.Text;
using System.Security.Cryptography;

namespace Fancy.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicatioonDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Hash the password
                string hashedPassword = Password(model.Password);

                // Create user object
                var user = new User
                {
                    UserName = model.Username,
                    PasswordHash = hashedPassword,
                    Email = model.Email,
                    IsAdmin = model.IsAdmin
                };

                // Add user to database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Redirect to login page or other page
                return RedirectToAction("Login");
            }

            // If ModelState is not valid, return the registration view with errors
            return View(model);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    

    [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(RegisterViewModel model)
        {
            return View(model);

        }


    }
}

