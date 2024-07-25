using Microsoft.EntityFrameworkCore;
using RazorPages_Pal_App.Data;
using RazorPages_Pal_App.Dto_s;
using RazorPages_Pal_App.Models;

namespace RazorPages_Pal_App.Service
{
    public class ProductionService
    {
        private readonly ApplicationDbContext _context;

        public ProductionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> Create(ProductionDto dto,string secret)
        {

            var store = await _context.stores
                .Where(x => x.ProductName.ToLower().Equals(dto.ProductName.ToLower())&&
                        x.Batch.ToLower().Equals(dto.Batch.ToLower()))
                .FirstOrDefaultAsync();
            if (store!=null)
            {
                Production production = new();
                production.ProductName = dto.ProductName;
                production.Batch = dto.Batch;
                production.Quantity = dto.Quantity;
                production.Tank = dto.Tank;
                production.FinalLevel = dto.FinalLevel;
                production.StoreId = store.Id;
                production.CreationTime = DateTime.SpecifyKind(dto.CreationTime, DateTimeKind.Utc);
                production.UserIdCreation = secret;
                production.UserIdModification = null;
                production.ModificacionTime = null;
                return "1";
            }
            return "Batch or product name is incorrect,try again!";
           
        }
    }
}
