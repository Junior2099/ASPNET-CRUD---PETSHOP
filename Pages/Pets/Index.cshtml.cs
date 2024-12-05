using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using caotinhofeliz.Data;
using caotinhofeliz.Models;

namespace caotinhofeliz.Pages_Pets
{
    public class IndexModel : PageModel
    {
        private readonly caotinhofeliz.Data.ApplicationDbContext _context;

        public IndexModel(caotinhofeliz.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Pet> Pet { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Pet = await _context.Pets
                .Include(p => p.Cliente).ToListAsync();
        }
    }
}
