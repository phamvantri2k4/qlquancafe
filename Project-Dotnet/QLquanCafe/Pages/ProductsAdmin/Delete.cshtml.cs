using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace QLquanCafe.Pages.ProductsAdmin
{
    public class DeleteModel : PageModel
    {
        private readonly QLquanCafe.Data.DbContect _context;

        public DeleteModel(QLquanCafe.Data.DbContect context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    Product = await _context.Products
        .Include(p => p.Category)
        .Include(p => p.ImgProducts)
        .FirstOrDefaultAsync(m => m.Id == id);

    if (Product == null)
    {
        return NotFound();
    }

    return Page();
}

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                Product = product;
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}