using System.ComponentModel.DataAnnotations;

namespace AspNetCore.FileUpload.Approaches.Models.ViewModels
{
    public class ProductStreamApproachV2ViewModel
    {
      
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public string? İmagePath { get; set; }
    }
}
