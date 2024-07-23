using Microsoft.EntityFrameworkCore;
using RazorPages_Pal_App.Data;
using RazorPages_Pal_App.Dto_s;
using RazorPages_Pal_App.Models;

namespace RazorPages_Pal_App.Service
{
    public class StoreService
    {
        private readonly ApplicationDbContext _context;

        public StoreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateStore(StoreDto storeDto,string secret)
        {
            try
            {
                Store store = new();
                store.ProductName = storeDto.ProductName;
                store.Batch = storeDto.Batch;
                store.Quantity = storeDto.Quantity;
                store.CreationTime = DateTime.SpecifyKind(storeDto.CreationTime, DateTimeKind.Utc);
                store.Description = storeDto.Description;
                store.Comments = storeDto.Comments;
                store.UserIdCreation = secret;
                store.ModificacionTime = null;
              //  store.UserIdModification = secret;
                await _context.stores.AddAsync(store);
                await _context.SaveChangesAsync();
                return "1";
            }
            catch (Exception e)
            {

                return e.ToString();
            }
        }

        public async Task<List<ResultStoreDto>> GetStoresWithUsernamesAsync()
        {
           /* var stores = await (from store in _context.stores
                                join userCreation in _context.Users on store.UserIdCreation equals userCreation.Id into uc
                                from userCreation in uc.DefaultIfEmpty()
                                join userModification in _context.Users on store.UserIdModification equals userModification.Id into um
                                from userModification in um.DefaultIfEmpty()
                                select new ResultStoreDto
                                {
                                    Id = store.Id,
                                    ProductName = store.ProductName,
                                    Batch = store.Batch,
                                    Quantity = store.Quantity,
                                    CreationTime = store.CreationTime,
                                    ModificacionTime = store.ModificacionTime.Value,
                                    Description = store.Description,
                                    Comments = store.Comments,
                                    UserIdCreation = store.UserIdCreation,
                                    UserNameCreation = userCreation.UserName ,
                                    UserIdModification = store.UserIdModification,
                                    UserNameModification = userModification.UserName
                                }).ToListAsync();*/
            
             var stores = await _context.stores
      .Include(store => store.UserCreation) // Incluir el usuario de creación
      .Include(store => store.UserModification) // Incluir el usuario de modificación
      .Select(store => new ResultStoreDto
      {
          Id = store.Id,
          ProductName = store.ProductName,
          Batch = store.Batch,
          Quantity = store.Quantity,
          CreationTime = store.CreationTime,
          ModificacionTime = store.ModificacionTime.Value, 
          Description = store.Description,
          Comments = store.Comments,
          UserIdCreation = store.UserIdCreation,
          UserNameCreation = store.UserCreation != null ? store.UserCreation.UserName : "Unknown", // Manejo de potencial null
          UserIdModification = store.UserIdModification,
          UserNameModification = store.UserModification != null ? store.UserModification.UserName : "Unknown" // Manejo de potencial null
      })
      .ToListAsync();

            

            return stores;
        }
        public async Task<List<Store>> GetStoresAsync()
        {
            try
            {
               var result = await _context.stores.ToListAsync();
                return result;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<ResultStoreDto> GetStoreByIdWitnName(int? id)
        {
            var stores = await _context.stores
         .Where(store => store.Id == id) // Filtra por el id proporcionado
         .Include(store => store.UserCreation) // Incluye el usuario de creación
         .Include(store => store.UserModification) // Incluye el usuario de modificación
         .Select(store => new ResultStoreDto
         {
             Id = store.Id,
             ProductName = store.ProductName,
             Batch = store.Batch,
             Quantity = store.Quantity,
             CreationTime = store.CreationTime,
             ModificacionTime = store.ModificacionTime.Value,
             Description = store.Description,
             Comments = store.Comments,
             UserIdCreation = store.UserIdCreation,
             UserNameCreation = store.UserCreation != null ? store.UserCreation.UserName : "Unknown",
             UserIdModification = store.UserIdModification,
             UserNameModification = store.UserModification != null ? store.UserModification.UserName : "Unknown"
         })
         .FirstOrDefaultAsync();

            return stores;


        }
        public async Task<Store> GetStoreById(int? id)
        {

            if (id==null)
            {
                return null;
            }
                var st = await _context.stores.FindAsync(id);
                return st;
                       
        }

        private async Task<bool> Exist(int? id)
        {
            try
            {
                var item = false;
                item = await _context.stores.AnyAsync(x => x.Id==id);
                return item;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<string> UpdateAsync(Store store) 
        {
            try
            {
               var exist = Exist(store.Id);
                if (exist.Equals(true))
                {
                    _context.stores.Update(store);
                    await _context.SaveChangesAsync();
                    return "1";
                }
                return "0";
            }
            catch (Exception e)
            {

                return e.Message;
            }
           
        }

        public async Task<bool> FindProductByName(string name){
            try
            {
                var a = await _context.stores.AnyAsync(x => x.ProductName == name);
                if (a == true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
