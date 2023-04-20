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

                    var item = contentDisposition; //öylesine koydum

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


        public async Task<(string?, ProductStreamApproachV2ViewModel)> UploadFileWithExplicitBinding(MultipartReader reader, MultipartSection section,ControllerBase controller)
        {
            string storagePath = "";
            string guidName = Guid.NewGuid().ToString();
            var root = _fileProvider.GetDirectoryContents("wwwroot");
            var images = root.First(x => x.Name == "images");
            var formAccumelator = new KeyValueAccumulator();


            while (section != null)
            {

                var hasContentDispositionHeader =
                       System.Net.Http.Headers.ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                var item = contentDisposition; //öylesine koydum
                if (hasContentDispositionHeader)
                {
                    if (contentDisposition.DispositionType.Equals("form-data") &&
                       (!string.IsNullOrEmpty(contentDisposition.FileName) ||
                       !string.IsNullOrEmpty(contentDisposition.FileNameStar)))
                    {
                        //file nameden tırnakları çıkardık
                        var myfilename = HeaderUtilities.RemoveQuotes(contentDisposition.FileName).Value;

                        //fiziksel kayıt yolut
                        var storagePathPhysical = Path.Combine(images.PhysicalPath!, guidName + Path.GetExtension(myfilename));

                        //veritabanı kayıt yolu
                        storagePath = guidName + Path.GetExtension(myfilename);


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
                    else
                    {
                        //model binding yapılacak yer eğer section dosya ile alakalı bir section değil ise
                        //burada bizim datalarımız ile alakalı ise burada modelbinding yaparıaz


                        //multipart olarak gelen model verilerimizi 
                        // contenten disposition bağlığından name değerini alıyoruz
                        //daha sonra section body'den değeri stream olarak okuyoruz
                        //key-value olarak alıyoruz
                        //key name değerimizi olacak form'daki prop adı
                        //value ise inputtadaki değeri
                        // keyvalue acumulator içine yerleştiriyoruz
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name).Value;

                        using (var streamReader = new StreamReader(section.Body,
                        encoding: Encoding.UTF8,
                        detectEncodingFromByteOrderMarks: true,
                        bufferSize: 1024,
                        leaveOpen: true))
                        {
                            var value = await streamReader.ReadToEndAsync();
                            if (string.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = string.Empty;
                            }
                            formAccumelator.Append(key, value);



                        }

                    }
                   
                    section = await reader.ReadNextSectionAsync();

                }


            }


            var product = new ProductStreamApproachV2ViewModel();
            //akumulatorden verileri çekiyoruz

            var formValueProvidere = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(formAccumelator.GetResults()),
                CultureInfo.CurrentCulture
            );
            //daha sonra verileri bind ediyoruz
            var bindindSuccessfully = await controller.TryUpdateModelAsync(product, "", formValueProvidere);




            return (storagePath,product);

        }
    }
}
