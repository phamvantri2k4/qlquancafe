using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLquanCafe.Models;

namespace QLquanCafe.Pages.Categoriess
{
    public class DeleteModel : PageModel
    {
        private readonly QLquanCafe.Data.DbContect _context;

        public DeleteModel(QLquanCafe.Data.DbContect context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;


        //hiển thị thông tin của một danh mục 
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categorys.FirstOrDefaultAsync(m => m.Id == id);//truy vấn dữ liệu và tìm danhmụcthôngquaid

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }

        // xóa danh mục khỏi csdl
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categorys.FindAsync(id); 
            if (category != null)
            {
                Category = category;
                _context.Categorys.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}