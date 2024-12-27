using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLquanCafe.Data;
using QLquanCafe.Models;

namespace QLquanCafe.Pages.Tables
{
    public class IndexModel : PageModel
    {
        private readonly DbContect _context;

        public IndexModel(DbContect context)
        {
            _context = context;
        }

        public IList<Table> Tables { get; set; }

        public async Task OnGetAsync()
        {
            Tables = await _context.Tables.ToListAsync();
        }
    }
}