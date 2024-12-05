using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using caotinhofeliz.Data;
using caotinhofeliz.Models;

namespace caotinhofeliz.Pages_Produtos
{
    public class EditModel : PageModel
    {
        private readonly caotinhofeliz.Data.ApplicationDbContext _context;

        public EditModel(caotinhofeliz.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Produto Produto { get; set; } = default!;

        [BindProperty]
        public IFormFile? ImagemUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            Produto = produto;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Atualiza os dados do produto
            var produtoAtual = await _context.Produtos.FindAsync(Produto.Id);
            if (produtoAtual == null)
            {
                return NotFound();
            }

            produtoAtual.Nome = Produto.Nome;
            produtoAtual.Descricao = Produto.Descricao;
            produtoAtual.Preco = Produto.Preco;

            // Se uma nova imagem foi carregada
            if (ImagemUpload != null)
            {
                var fileName = $"{Produto.Id}_{ImagemUpload.FileName}";
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Salva a imagem no servidor
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagemUpload.CopyToAsync(fileStream);
                }

                produtoAtual.ImagemUrl = $"/uploads/{fileName}";
            }

            _context.Attach(produtoAtual).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(Produto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
