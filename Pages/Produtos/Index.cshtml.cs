using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using caotinhofeliz.Data;
using caotinhofeliz.Models;

namespace caotinhofeliz.Pages_Produtos
{
    public class IndexModel : PageModel
    {
        private readonly caotinhofeliz.Data.ApplicationDbContext _context;

        public IndexModel(caotinhofeliz.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Produto> Produto { get; set; } = new List<Produto>();

        public async Task OnGetAsync()
        {
            Produto = await _context.Produtos.ToListAsync();
        }
    }
}