using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages_Pal_App.Dto_s;
using RazorPages_Pal_App.Models;
using RazorPages_Pal_App.Service;

namespace RazorPages_Pal_App.Pages.StorePages
{
    public class DetailsModel : PageModel
    {
        private readonly StoreService _storeService;

        public DetailsModel(StoreService storeService)
        {
            _storeService=storeService;
        }

        [BindProperty]
        public string Messages { get; set; }

        [BindProperty]
        public updateStoreDto Store { get; set; } 
        public async Task<IActionResult> OnGet(int? id)
        {
            try
            {
                if (id==null)
                {
                    Messages = "Not Found!";
                    return Page();
                }
                Store = await _storeService.GetStoreByIdWitnName(id);
                return Page();
            }
            catch (Exception e)
            {

                Messages = e.ToString();
                return Page();
            }

        }
    }
}
