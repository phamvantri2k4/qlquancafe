using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QLquanCafe.Data;
using QLquanCafe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QLquanCafe.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly DbContect _context;

        public IndexModel(DbContect context)
        {
            _context = context;
        }

        public IList<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.User)
                .ToListAsync();
        }
    }
}