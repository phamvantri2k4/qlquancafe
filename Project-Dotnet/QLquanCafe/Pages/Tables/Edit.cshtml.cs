using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLquanCafe.Data;
using QLquanCafe.Models;

namespace QLquanCafe.Pages.Tables
{
    public class EditModel : PageModel
    {
        private readonly DbContect _context;

        public EditModel(DbContect context)
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

            var tableToUpdate = await _context.Tables.FirstOrDefaultAsync(t => t.TableId == id);

            if (tableToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Table>(
                tableToUpdate,
                "table",
                t => t.TableName, t => t.Status))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableExists(tableToUpdate.TableId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return Page();
        }

        private bool TableExists(int id)
        {
            return _context.Tables.Any(e => e.TableId == id);
        }
    }
}