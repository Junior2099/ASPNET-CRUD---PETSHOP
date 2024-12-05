using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using caotinhofeliz.Data;
using caotinhofeliz.Models;

namespace caotinhofeliz.Pages_Produtos
{
    public class DetailsModel : PageModel
    {
        private readonly caotinhofeliz.Data.ApplicationDbContext _context;

        public DetailsModel(caotinhofeliz.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Produto Produto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FirstOrDefaultAsync(m => m.Id == id);

            if (produto is not null)
            {
                Produto = produto;

                return Page();
            }

            return NotFound();
        }
    }
}