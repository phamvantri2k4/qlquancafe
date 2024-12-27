using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using QLquanCafe.Models;
using System.ComponentModel.DataAnnotations;

namespace QLquanCafe.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        // Một danh mục có thể có nhiều sản phẩm
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
