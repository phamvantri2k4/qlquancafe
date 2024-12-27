using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using QLquanCafe.Data;
using QLquanCafe.Models;
using static QLquanCafe.Models.User;

namespace QLquanCafe.Pages
{
    public class LoginModel : PageModel
    {
        private readonly DbContect _context;

        public LoginModel(DbContect context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ErrorMessage { get; set; }

        public class InputModel
        {
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
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == Input.Email && u.Password == Input.Password);

                if (user != null)
                {
                    // Tạo danh sách các claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString()), // Thêm vai trò
                        new Claim("Name", user.Name) // Thêm tên đầy đủ
                    };

                    // Tạo ClaimsIdentity và ClaimsPrincipal
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // Đăng nhập bằng cookie authentication
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal);

                    // Phân quyền: Chuyển hướng dựa trên vai trò
                    if (user.Role == UserRole.Admin)
                    {
                        return RedirectToPage("/ProductsAdmin/Index");
                    }
                    else if (user.Role == UserRole.User)
                    {
                        return RedirectToPage("Index");
                    }

                    // Trường hợp không xác định được quyền
                    ErrorMessage = "Không xác định được quyền người dùng.";
                    return Page();
                }

                // Nếu không tìm thấy người dùng hoặc sai mật khẩu
                ErrorMessage = "Email hoặc mật khẩu không hợp lệ.";
            }

            return Page();
        }
    }
}