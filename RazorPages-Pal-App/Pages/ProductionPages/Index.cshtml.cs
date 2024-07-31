using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using RazorPages_Pal_App.Data;
using RazorPages_Pal_App.Dto_s;
using RazorPages_Pal_App.Models;
using RazorPages_Pal_App.Service;

namespace RazorPages_Pal_App.Pages.ProductionPages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ProductionService _productionService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ProductionService productionService,
                           UserManager<ApplicationUser> userManager,
                           ApplicationDbContext context)
        {
            _productionService = productionService;
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public List<ResultProductionDto> list { get; set; } = default;
        public string Messages { get; set; }
        public async Task OnGetAsync()
        {
            try
            {
                list = await _productionService.GetProductions();
                Console.WriteLine("Date : " + DateTime.Now + "Consulta Ok en production index "+list.ToJson());

            }
            catch (Exception e)
            {

                Messages = e.Message;
                Console.WriteLine("Date : " + DateTime.Now + ",Error en Production-Index ,Error : " + e.Message);
                
            }
           

            
        }
    }
}
