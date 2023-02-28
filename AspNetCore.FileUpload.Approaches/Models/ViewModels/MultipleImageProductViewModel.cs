namespace AspNetCore.FileUpload.Approaches.Models.ViewModels
{
    public class MultipleImageProductViewModel
    {

        public string Name { get; set; }
        public List<IFormFile>? Images { get; set; }
        
        public List<string>? Paths { get; set; }

    }
}
