using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using caotinhofeliz.Models;
using caotinhofeliz.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace caotinhofeliz.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ApplicationDbContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Cliente Cliente { get; set; } = default!;

        public IActionResult OnGet()
        {
            _logger.LogInformation("OnGet called");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync called");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid");
                return Page();
            }

            _context.Clientes.Add(Cliente);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Cliente criado com sucesso: {@Cliente}", Cliente);

            return RedirectToPage("./Index");
        }
    }
}