namespace AspNetCore.FileUpload.Approaches.Models
{
    public class MultipleImageProductPath
    {
        public int Id { get; set; }

        public int MultipleImageProductId { get; set; }
        public string Path { get; set; }

        public MultipleImageProduct MultipleImageProduct { get; set; }
    }
}
