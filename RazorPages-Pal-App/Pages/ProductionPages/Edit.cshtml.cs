using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages_Pal_App.Dto_s;
using RazorPages_Pal_App.Service;

namespace RazorPages_Pal_App.Pages.ProductionPages
{
    public class EditModel : PageModel
    {
        private readonly ProductionService _productionService;
        public EditModel(ProductionService productionService)
        {
            _productionService = productionService;
        }

        public string Messages { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        [BindProperty]
        public ResultProductionDto? productionDto { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id != null)
            {
                Id = id;
                productionDto = await _productionService.GetProductionsById(id);
               
            }
            Messages = "Record Not found!";
            return Page();
           

        }
    }
}
