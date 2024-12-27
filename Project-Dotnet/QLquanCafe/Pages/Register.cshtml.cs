using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using QLquanCafe.Data;
using QLquanCafe.Models;

namespace QLquanCafe.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly DbContect _context;

        public RegisterModel(DbContect context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new User
            {
                Name = Input.Name,
                Email = Input.Email,
                Password = Input.Password // B?n nên hash m?t kh?u tr??c khi l?u
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Login");
        }
    }
}