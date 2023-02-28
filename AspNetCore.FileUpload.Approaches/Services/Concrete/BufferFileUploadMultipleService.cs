using AspNetCore.FileUpload.Approaches.Services.Abstract;
using Microsoft.Extensions.FileProviders;

namespace AspNetCore.FileUpload.Approaches.Services.Concrete
{
    public class BufferFileUploadMultipleService : IBufferFileUploadMultipleApproachService
    {
        private readonly IFileProvider _fileProvider;

        public BufferFileUploadMultipleService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }


        //if files upload is succsed return pathlist
        //else return null
        public async Task<List<string>?> UploadFileMultipleApproachAsync(IEnumerable<IFormFile> files)
        {
            var pathList = new List<string>();
            var root = _fileProvider.GetDirectoryContents("wwwroot");
            var images = root.First(x => x.Name == "images");

            try
            {
                var result = true;
                foreach (var item in files)
                {

                    if (item.Length > 0 && item.Length < 2000000 && ( Path.GetExtension(item.FileName) == ".png" || Path.GetExtension(item.FileName) == ".jpg"))
                    {

                        var guidNameWithExtention = Guid.NewGuid() +Path.GetExtension(item.FileName);
                        pathList.Add(guidNameWithExtention);
                        var storagePath = Path.Combine(images.PhysicalPath!, guidNameWithExtention);
                        using var stream = new FileStream(storagePath, FileMode.Create);
                        await item.CopyToAsync(stream);

                    }
                    else
                    {
                        result= false;
                        break;
                    }

                }

                return result ? pathList : null;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
