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
    public class DetailsModel : PageModel
    {
        private readonly caotinhofeliz.Data.ApplicationDbContext _context;

        public DetailsModel(caotinhofeliz.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Pet Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FirstOrDefaultAsync(m => m.Id == id);

            if (pet is not null)
            {
                Pet = pet;

                return Page();
            }

            return NotFound();
        }
    }
}
