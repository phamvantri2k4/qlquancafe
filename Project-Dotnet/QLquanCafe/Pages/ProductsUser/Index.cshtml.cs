using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLquanCafe.Models;

namespace QLquanCafe.Pages.ProductsUser
{
    public class IndexModel : PageModel
    {
        private readonly QLquanCafe.Data.DbContect _context;

        public IndexModel(QLquanCafe.Data.DbContect context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;
        public string SearchString { get; set; }
        public int? CategoryId { get; set; }

        public async Task OnGetAsync(string searchString, int? categoryId)
        {
            SearchString = searchString;
            CategoryId = categoryId;

            var products = from p in _context.Products
                           .Include(p => p.Category)
                           .Include(p => p.ImgProducts)
                           select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(p => p.Name.Contains(SearchString));
            }

            if (CategoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == CategoryId);
            }

            Products = await products.ToListAsync();

            // Lấy danh sách danh mục và thiết lập ViewData["Categories"]
            var categories = await _context.Categorys.ToListAsync();
            ViewData["Categories"] = categories;
        }

        public async Task<IActionResult> OnPostOrderAsync(int productId)
        {
            // Xử lý logic đặt hàng ở đây
            // Ví dụ: Thêm sản phẩm vào giỏ hàng hoặc tạo đơn hàng mới

            return RedirectToPage();
        }
    }
}