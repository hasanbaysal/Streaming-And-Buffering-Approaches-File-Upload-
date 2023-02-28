

using System.ComponentModel.DataAnnotations;

namespace AspNetCore.FileUpload.Approaches.Models.ViewModels
{
    public class SingleImageProductViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
