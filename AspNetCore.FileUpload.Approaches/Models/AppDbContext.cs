using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel;

namespace AspNetCore.FileUpload.Approaches.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<SingleImageProduct> SingleImageProducts { get; set; }
        public DbSet<MultipleImageProductPath> MultipleImageProductPaths { get; set; }
        public DbSet<MultipleImageProduct> MultipleImageProducts { get; set; }
        public DbSet<StreamUploadImageProduct> StreamUploadImageProducts { get; set; }
    }
}
