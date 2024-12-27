using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace QLquanCafe.Models
{
    public class Order
    {
        [Key]
        public int OrdersId { get; set; } // Khóa chính của bảng Order

        public DateTime OrderDate { get; set; } // Ngày tạo đơn hàng

        // Khóa ngoại đến bảng Table
        public int TableId { get; set; }

        [ValidateNever]
        public Table Table { get; set; } // Navigation property cho bảng Table

        // Khóa ngoại đến bảng User
        public int UserId { get; set; } // Sửa tên từ Id thành UserId để rõ ràng hơn

        [ValidateNever]
        public User User { get; set; } // Navigation property cho bảng User

        public decimal TotalAmount { get; set; } // Tổng tiền của đơn hàng

        public bool Status { get; set; } // Trạng thái đơn hàng
    }
}
