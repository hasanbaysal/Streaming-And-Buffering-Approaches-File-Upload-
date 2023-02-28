using AspNetCore.FileUpload.Approaches.Models.ViewModels;
using AspNetCore.FileUpload.Approaches.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspNetCore.FileUpload.Approaches.Services.Concrete
{
    public class StreamFileUploadServices : IStreamFileUploadService
    {


        private readonly IFileProvider _fileProvider;

        public StreamFileUploadServices(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<string?> UploadFile(MultipartReader reader, MultipartSection section)
        {
            string storagePath = "";
            string guidName = Guid.NewGuid().ToString();
            var root = _fileProvider.GetDirectoryContents("wwwroot");
            var images = root.First(x => x.Name == "images");


            try
            {
                while (section != null)
                {
                    var hasContentDispositionHeader =
                        System.Net.Http.Headers.ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (contentDisposition.DispositionType.Equals("form-data") &&
                        (!string.IsNullOrEmpty(contentDisposition.FileName) ||
                        !string.IsNullOrEmpty(contentDisposition.FileNameStar)))
                        {
                            //
                            var fileName = contentDisposition.FileName;
                        
                            fileName= fileName!.Remove(0, 1);
                             fileName= fileName.Remove(fileName.Length-1, 1);
                            Console.WriteLine("filename  : " + fileName);
                         
                         var storagePathPhysical = Path.Combine(images.PhysicalPath!, guidName + Path.GetExtension(fileName));

                            storagePath = guidName + Path.GetExtension(fileName);

                            //


                            byte[] fileArray;
                            using (var memoryStream = new MemoryStream())
                            {
                                await section.Body.CopyToAsync(memoryStream);
                                fileArray = memoryStream.ToArray();
                            }

                            using (var fileStream = System.IO.File.Create(storagePathPhysical))
                            {
                                await fileStream.WriteAsync(fileArray);
                            }
                        }
                    }
                  

                    section = await reader.ReadNextSectionAsync();

                }


                return storagePath;

            }
            catch (Exception)
            {

                return null;
               
            }
          


         

        }
    }
}
