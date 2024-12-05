using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using caotinhofeliz.Data;
using caotinhofeliz.Models;

namespace caotinhofeliz.Pages_Produtos
{
    public class DeleteModel : PageModel
    {
        private readonly caotinhofeliz.Data.ApplicationDbContext _context;

        public DeleteModel(caotinhofeliz.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                Produto = produto;

                // Excluir a imagem associada, se existir
                if (!string.IsNullOrEmpty(Produto.ImagemUrl))
                {
                    var imagePath = Path.Combine("wwwroot", Produto.ImagemUrl.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                // Remover o produto do banco de dados
                _context.Produtos.Remove(Produto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}