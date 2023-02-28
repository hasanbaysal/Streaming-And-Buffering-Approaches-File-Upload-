using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AspNetCore.FileUpload.Approaches.Models
{
    public class MultipleImageProduct
    {
        public MultipleImageProduct()
        {
            MultipleImageProductPaths = new();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MultipleImageProductPath> MultipleImageProductPaths { get; set; }
    }
}
