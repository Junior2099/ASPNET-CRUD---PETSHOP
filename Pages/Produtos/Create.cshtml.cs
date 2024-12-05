using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using caotinhofeliz.Data;
using caotinhofeliz.Models;

namespace caotinhofeliz.Pages_Produtos
{
    public class CreateModel : PageModel
    {
        private readonly caotinhofeliz.Data.ApplicationDbContext _context;

        public CreateModel(caotinhofeliz.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Produto Produto { get; set; } = default!;

        [BindProperty]
        public IFormFile? Imagem { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Imagem != null)
            {
                // Gerar um nome único para o arquivo
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Imagem.FileName);
                var uploadPath = Path.Combine("wwwroot/images", fileName);

                // Salvar o arquivo no diretório
                using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                {
                    await Imagem.CopyToAsync(fileStream);
                }

                // Salvar o caminho da imagem no banco de dados
                Produto.ImagemUrl = $"/images/{fileName}";
            }

            _context.Produtos.Add(Produto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}