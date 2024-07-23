using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages_Pal_App.Dto_s;
using RazorPages_Pal_App.Models;
using RazorPages_Pal_App.Service;

namespace RazorPages_Pal_App.Pages.StorePages
{
    public class IndexModel : PageModel
    {
        private readonly StoreService _storeService;

        public IndexModel(StoreService storeService)
        {
            _storeService = storeService;
        }
        
       // [BindProperty]
        public string Messages { get; set; }
        [BindProperty]
        public List<ResultStoreDto> result { get; set; }
        public async Task<IActionResult> OnGet()
        {
            result = await _storeService.GetStoresWithUsernamesAsync();
            if (result == null)
            {
                Messages = "Data not found, try agin...";
                return Page();
            }

            return Page();
        }
    }


}
