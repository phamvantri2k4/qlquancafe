using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using QLquanCafe.Models;
using System.ComponentModel.DataAnnotations;
namespace QLquanCafe.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        [ValidateNever]
        public Order Order { get; set; }
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        public int QuanTity { get; set; }
        public decimal Price { get; set; }
    }
}
