using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QLquanCafe.Data;
using QLquanCafe.Models;

namespace QLquanCafe.Pages.Tables
{
    public class CreateModel : PageModel
    {
        private readonly DbContect _context;

        public CreateModel(DbContect context)
        {
            _context = context;
        }

        [BindProperty]
        public Table Table { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tables.Add(Table);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}