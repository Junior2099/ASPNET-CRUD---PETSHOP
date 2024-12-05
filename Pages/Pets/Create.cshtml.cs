using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using caotinhofeliz.Models;
using caotinhofeliz.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Collections.Generic;

namespace caotinhofeliz.Pages_Pets
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
        public Pet Pet { get; set; } = new Pet();

        public List<SelectListItem> Clientes { get; set; } = new List<SelectListItem>();

        public IActionResult OnGet()
        {
            _logger.LogInformation("OnGet called");

            var clientes = _context.Clientes.ToList();
            if (clientes == null || !clientes.Any())
            {
                _logger.LogWarning("Nenhum cliente encontrado. Certifique-se de que há clientes cadastrados no sistema.");
                throw new System.Exception("Nenhum cliente encontrado. Por favor, cadastre clientes antes de adicionar pets.");
            }

            _logger.LogInformation("Clientes carregados: {ClientesCount}", clientes.Count);
            Clientes = clientes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nome
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("OnPostAsync called");

            // Log detalhes dos dados recebidos
            _logger.LogInformation("Pet recebido: {@Pet}", Pet);

            if (Pet.ClienteId == 0)
            {
                _logger.LogWarning("ClienteId é 0, adicionando erro de validação.");
                ModelState.AddModelError("Pet.ClienteId", "Por favor, selecione um cliente.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid");

                // Log detalhes do ModelState
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        _logger.LogWarning("Erro de validação em '{Key}': {ErrorMessage}", key, error.ErrorMessage);
                    }
                }

                var clientes = _context.Clientes.ToList();
                if (clientes == null || !clientes.Any())
                {
                    _logger.LogWarning("Nenhum cliente encontrado.");
                }
                else
                {
                    _logger.LogInformation("Clientes carregados: {ClientesCount}", clientes.Count);
                    Clientes = clientes.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nome
                    }).ToList();
                }

                return Page();
            }

            try
            {
                _context.Pets.Add(Pet);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Pet criado com sucesso: {@Pet}", Pet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar o pet no banco de dados.");
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
