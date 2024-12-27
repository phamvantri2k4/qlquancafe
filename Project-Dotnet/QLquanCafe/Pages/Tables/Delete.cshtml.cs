using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLquanCafe.Data;
using QLquanCafe.Models;

namespace QLquanCafe.Pages.Tables
{
    public class DeleteModel : PageModel
    {
        private readonly DbContect _context;

        public DeleteModel(DbContect context)
        {
            _context = context;
        }

        [BindProperty]
        public Table Table { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Table = await _context.Tables.FirstOrDefaultAsync(m => m.TableId == id);

            if (Table == null)
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

            Table = await _context.Tables.FindAsync(id);

            if (Table != null)
            {
                _context.Tables.Remove(Table);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}