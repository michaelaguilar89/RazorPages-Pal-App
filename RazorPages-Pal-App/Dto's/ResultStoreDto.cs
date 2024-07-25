using RazorPages_Pal_App.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RazorPages_Pal_App.Dto_s
{
    public class ResultStoreDto
    {
        
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Batch { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        public string? ModificacionTime { get; set; }
        
        public string? Description { get; set; }
        
        public string? Comments { get; set; }
        [Required]
        public string? UserIdCreation { get; set; }
        public string? UserNameCreation { get; set; }

        public string? UserIdModification { get; set; }
        public string? UserNameModification { get; set; }
       

    }
}
