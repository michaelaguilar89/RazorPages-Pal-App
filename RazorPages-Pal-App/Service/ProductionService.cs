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
        public async Task<List<Production>> GetProductions()
        {
            try
            {
                var prod = await _context.productions.ToListAsync();
                return prod;
                
            }
            catch (Exception e)
            {
             
                Console.WriteLine("Date : "+DateTime.Now+" Error : " + e.Message);
                return null;
            }
            
        }
        public async Task<string> Create(ProductionDto dto,string secret)
        {
            try
            {
                //1 . buscar el productname , batch
                var store = await _context.stores
                .Where(x => x.ProductName.ToLower().Equals(dto.ProductName.ToLower()) &&
                        x.Batch.ToLower().Equals(dto.Batch.ToLower()))
                .FirstOrDefaultAsync();

                if (store != null)//si el name y batch coinciden...
                {
                    Production production = new();
                    production.ProductName = dto.ProductName;
                    production.Batch = dto.Batch;
                    production.Quantity = dto.Quantity;
                    production.Tank = dto.Tank;
                    production.FinalLevel = dto.FinalLevel;
                    production.Comments = dto.Comments;
                    production.StoreId = store.Id;//obtener id de record en stores
                    production.CreationTime = DateTime.SpecifyKind(dto.CreationTime, DateTimeKind.Utc);
                    production.UserIdCreation = secret;
                    production.UserIdModification = null;
                    production.ModificacionTime = null;
                    await _context.productions.AddAsync(production);//guardo el record del production
                    var quantity = store.ActualQuantity - dto.Quantity;
                    Console.WriteLine("quantity : "+quantity +" - "+dto.Quantity);
                    store.ActualQuantity = quantity;
                    _context.stores.Update(store);//actualizo la cantidad del store
                    await _context.SaveChangesAsync();
                    return "1";
                }
                return "Batch or product name is incorrect,try again!";
            }
            catch (Exception e)
            {
            
                return e.Message;
            }
            
           
        }
    }
}
