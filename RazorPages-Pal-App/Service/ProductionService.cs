﻿using Microsoft.EntityFrameworkCore;
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


        public async Task<ResultProductionDto> GetProductionsById(int? id)
        {
            try
            {

                var data = await _context.Productions
                    .Include(user => user.UserCreation)//incluir el username create
                    .Include(user => user.UserModification)//incluir el username modification
                    .Where(x => x.Id == id)
                    .Select(p => new ResultProductionDto
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Batch = p.Batch,
                        StoreId = p.StoreId,
                        Quantity = p.Quantity,
                        Tank = p.Tank,
                        FinalLevel = p.FinalLevel,
                        CreationTime = p.CreationTime,
                        ModificacionTime = p.ModificacionTime,
                        Comments = p.Comments,
                        UserIdCreation = p.UserIdCreation,
                        UserNameCreation = p.UserCreation.UserName != null ? p.UserCreation.UserName : "Unknown",
                        UserIdModification = p.UserIdModification != null ? p.UserCreation.UserName : "Unknown",
                        UserNameModification = p.UserModification.UserName != null ? p.UserModification.UserName : "Unknown" // Manejo de potencial null,
                      
                    }).FirstOrDefaultAsync();
                return data;

            }
            catch (Exception e)
            {

                Console.WriteLine("Date : " + DateTime.Now + " Error : " + e.Message);
                return null;
            }

        }

        public async Task<List<ResultProductionDto>> GetProductions()
        {
            try
            {
                var prod = await _context.Productions
                    .Include(user => user.UserCreation)//incluir el username create
                    .Include(user => user.UserModification)//incluier el username modification
                    .Select(p => new ResultProductionDto
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Batch = p.Batch,
                        StoreId = p.StoreId,
                        Quantity = p.Quantity,
                        Tank = p.Tank,
                        FinalLevel = p.FinalLevel,
                        CreationTime = p.CreationTime,
                        ModificacionTime = p.ModificacionTime,
                        Comments = p.Comments,
                        UserIdCreation = p.UserIdCreation,
                        UserNameCreation = p.UserCreation.UserName != null ? p.UserCreation.UserName : "Unknown",
                        UserIdModification = p.UserIdModification != null ? p.UserCreation.UserName : "Unknown",
                        UserNameModification = p.UserModification.UserName != null ? p.UserModification.UserName : "Unknown" // Manejo de potencial null,
                      
                    }).ToListAsync();
                // await _context.DisposeAsync();
                return prod;

            }
            catch (Exception e)
            {

                Console.WriteLine("Date : " + DateTime.Now + " Error : " + e.Message);
                return null;
            }

        }
        public async Task<string> Create(ProductionDto dto, string secret)
        {
            // Usar transacciones asíncronas
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                string message = "0";

                // Buscar si existe un producto con el mismo nombre y lote
                var store = await _context.Stores
                    .Where(x => x.ProductName.ToLower().Equals(dto.ProductName.ToLower()) &&
                            x.Batch.ToLower().Equals(dto.Batch.ToLower()))
                    .FirstOrDefaultAsync();

                if (store != null)
                {
                    Production production = new()
                    {
                        ProductName = dto.ProductName,
                        Batch = dto.Batch,
                        Quantity = dto.Quantity,
                        Tank = dto.Tank,
                        FinalLevel = dto.FinalLevel,
                        Comments = dto.Comments,
                        StoreId = store.Id, // Obtener ID del record en Stores
                        CreationTime = DateTime.SpecifyKind(dto.CreationTime, DateTimeKind.Utc),
                        UserIdCreation = secret,
                        UserIdModification = null,
                        ModificacionTime = null
                    };

                    // Guardar el record de Production
                    await _context.Productions.AddAsync(production);

                    // Actualizar la cantidad en el Store
                    var quantity = store.ActualQuantity - dto.Quantity;
                    Console.WriteLine("quantity : " + quantity + " - " + dto.Quantity);
                    store.ActualQuantity = quantity;
                    _context.Stores.Update(store);

                    // Guardar cambios en la base de datos
                    await _context.SaveChangesAsync();

                    // Confirmar la transacción
                    await transaction.CommitAsync();
                    return "1";
                }

                // Si el lote o nombre es incorrecto, retornar mensaje
                return "Batch or product name is incorrect, try again!";
            }
            catch (Exception e)
            {
                // Rollback en caso de excepción
                await transaction.RollbackAsync();
                return e.Message;
            }
        }
    }

}       
            
           
       

