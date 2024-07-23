using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPages_Pal_App.Data;
using RazorPages_Pal_App.Dto_s;
using RazorPages_Pal_App.Service;

namespace RazorPages_Pal_App.Pages.StorePages
{
    public class EditModel : PageModel
    {
        private readonly StoreService _storeService;

        public EditModel(StoreService storeService)
        {
            _storeService = storeService;
        }

        [BindProperty]
        public ResultStoreDto StoreDto { get; set; } = default!;

        public string Messages { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultstoredto =  await _storeService.GetStoreByIdWitnName(id);
            if (resultstoredto == null)
            {
                return NotFound();
            }
            StoreDto = resultstoredto;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Messages = "Model Invalid";
                return Page();

            }
            Messages = "Model is oK";
            return Page();
           

        }

      /*  private bool ResultStoreDtoExists(int id)
        {
            return _context.ResultStoreDto.Any(e => e.Id == id);
        }*/
    }
}
