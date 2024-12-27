using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using QLquanCafe.Data;
using QLquanCafe.Models;

namespace QLquanCafe.Pages
{
    public class SearchModel : PageModel
    {
        private readonly DbContect _context;

        public SearchModel(DbContect context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; }
        public IList<Category> Categories { get; set; }
        public string SearchCategory { get; set; }

        public async Task OnGetAsync(string searchCategory)
        {
            // Lấy danh sách danh mục
            Categories = await _context.Categorys.ToListAsync();

            // Lọc sản phẩm theo danh mục nếu có tham số tìm kiếm
            if (!string.IsNullOrEmpty(searchCategory))
            {
                Products = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.Category.Name.Contains(searchCategory))
                    .ToListAsync();
                SearchCategory = searchCategory;
            }
            else
            {
                // Hiển thị toàn bộ sản phẩm nếu không tìm kiếm
                Products = await _context.Products.Include(p => p.Category).ToListAsync();
            }
        }
    }
}
