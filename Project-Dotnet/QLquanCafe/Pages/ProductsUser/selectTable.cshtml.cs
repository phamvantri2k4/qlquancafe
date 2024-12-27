using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLquanCafe.Data;
using QLquanCafe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLquanCafe.Pages.ProductsUser
{
    public class SelectTableModel : PageModel
    {
        private readonly DbContect _context;

        public SelectTableModel(DbContect context)
        {
            _context = context;
        }

        public IList<Table> Tables { get; set; }
        public int Id { get; set; }

        public async Task OnGetAsync(int id)
        {
            Id = id;
            Tables = await _context.Tables.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int id, int tableId)
        {
            var product = await _context.Products.FindAsync(id);
            var table = await _context.Tables.FindAsync(tableId);

            if (product == null || table == null)
            {
                return NotFound();
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                TableId = tableId,
                UserId = 1, // Giả sử UserId là 1 cho ví dụ
                TotalAmount = product.Price,
                Status = false
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderDetails = new OrderDetails
            {
                OrderId = order.OrdersId,
                ProductId = id, // Sử dụng ProductId để liên kết tới Id của Product
                QuanTity = 1,
                Price = product.Price
            };

            _context.orderDetails.Add(orderDetails);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Orders/Index");
        }
    }
}