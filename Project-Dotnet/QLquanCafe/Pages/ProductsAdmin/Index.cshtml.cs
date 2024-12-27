using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLquanCafe.Data;
using QLquanCafe.Models;

namespace QLquanCafe.Pages.ProductsAdmin
{
    public class IndexModel : PageModel
    {
        private readonly QLquanCafe.Data.DbContect _context;

        // Danh sách danh mục cho dropdown
        public SelectList Categories { get; set; } = default!;

        // Giá trị tìm kiếm
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        // Lọc theo danh mục
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        public IndexModel(QLquanCafe.Data.DbContect context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                // Truy vấn danh sách sản phẩm bao gồm thông tin danh mục và hình ảnh
                var products = _context.Products
                    .Include(p => p.Category) // Bao gồm thông tin danh mục
                    .Include(p => p.ImgProducts) // Bao gồm thông tin hình ảnh
                    .AsQueryable();

                // Lấy danh sách danh mục cho dropdown
                var categories = await _context.Categorys.ToListAsync();
                Categories = new SelectList(categories, "Id", "Name");
                ViewData["Categories"] = categories;

                // Lọc theo từ khóa tìm kiếm
                if (!string.IsNullOrEmpty(SearchString))
                {
                    products = products.Where(p => p.Name.Contains(SearchString));
                }

                //Lọc theo danh mục nếu có giá trị CategoryId
                 if (CategoryId.HasValue && CategoryId.Value != 0)
                {
                    products = products.Where(p => p.CategoryId == CategoryId.Value);
                }

                // Cập nhật danh sách sản phẩm sau khi lọc
                Product = await products.ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, display a message)
                ModelState.AddModelError(string.Empty, "An error occurred while retrieving data.");
            }
        }
    }
}