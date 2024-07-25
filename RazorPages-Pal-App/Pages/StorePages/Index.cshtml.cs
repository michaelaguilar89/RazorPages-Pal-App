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
        [BindProperty(SupportsGet = true)]
        public string? search { get; set; }
        [BindProperty(SupportsGet =true)]
        public DateTime? searchDate { get; set; }
        public async Task<IActionResult> OnGet(string? searchString, DateTime? searchDateValue)
        {

            
            try
            {

                searchDate = searchDateValue;
                search = searchString;

                if (searchDate != null)
                 {
                    
                     result = await _storeService.getStoresByDate(searchDate);
                 }
                 else
                 {
                     if (searchDateValue != null && search != null)
                     {
                       
                         result = await _storeService.getStoresByNameOrBatchAndDate(searchString, searchDate);
                     }
                     else
                     {
                         if (searchString != null)
                         {
                             
                             result = await _storeService.getStoresByNameOrBatch(search);

                         }
                         else {
                             result = await _storeService.GetStoresWithUsernamesAsync();
                         }
                     }




                 }


                 if (result == null)
             {
                 Messages = "Data not found, try agin...";

             }
            return Page();
            }
            catch (Exception e)
            {
                Messages = "Error : " + e.Message;
                return Page();
            }
        }
    }


}
