using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages_Pal_App.Models;
using RazorPages_Pal_App.Service;

namespace RazorPages_Pal_App.Pages.ProductionPages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ProductionService _productionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ProductionService productionService,
                           UserManager<ApplicationUser> userManager)
        {
            _productionService = productionService;
            _userManager = userManager;
        }

        [BindProperty]
        public List<Production> list { get; set; } = default;
        public string Messages { get; set; }
        public async void OnGetAsync()
        {
            try
            {
                list = await _productionService.GetProductions();
            
                
            }
            catch (Exception e)
            {

                Messages = e.Message;
                Console.WriteLine("Date : " + DateTime.Now + " Error : " + e.Message);
                
            }
           

            
        }
    }
}
