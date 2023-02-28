using AspNetCore.FileUpload.Approaches.Services.Abstract;
using Microsoft.Extensions.FileProviders;

namespace AspNetCore.FileUpload.Approaches.Services.Concrete
{
    public class BufferFileUploadSingleService : IBufferFileUploadSingleApproachService
    {
		private readonly IFileProvider _fileProvider;
		
        public BufferFileUploadSingleService(IFileProvider file)
        {
            _fileProvider = file;
        }
     

       
        public async Task<bool> UploadFile(IFormFile file, string guildNameWithExtention)
        {
			try
			{
                if (file.Length > 0 && file.Length < 2000000 && (Path.GetExtension(file.FileName) == ".png" || Path.GetExtension(file.FileName) == ".jpg"))
                {
                    var root = _fileProvider.GetDirectoryContents("wwwroot");
                    var images = root.First(x => x.Name == "images");

                    var storagePath = Path.Combine(images.PhysicalPath!, guildNameWithExtention);

                    using var stream = new FileStream(storagePath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    return true;


                }
                else { return false; }
			}
			catch (Exception)
			{

                return false;
			}
        }
    }
}
