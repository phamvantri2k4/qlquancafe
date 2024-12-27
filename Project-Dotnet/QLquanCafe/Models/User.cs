using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using QLquanCafe.Models;
using System.ComponentModel.DataAnnotations;
namespace QLquanCafe.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
         public enum UserRole
        {
            Admin = 1,
            User = 0
        }
        
    }
}
