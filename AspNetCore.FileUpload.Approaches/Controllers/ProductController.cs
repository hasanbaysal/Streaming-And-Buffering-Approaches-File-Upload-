using AspNetCore.FileUpload.Approaches.Migrations;
using AspNetCore.FileUpload.Approaches.Models;
using AspNetCore.FileUpload.Approaches.Models.ViewModels;
using AspNetCore.FileUpload.Approaches.Services.Abstract;
using AspNetCore.FileUpload.Approaches.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using NuGet.Packaging;
using System.Runtime.CompilerServices;

namespace AspNetCore.FileUpload.Approaches.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBufferFileUploadSingleApproachService _singleBufferUpload;
        private readonly IBufferFileUploadMultipleApproachService _multipleBufferUpload;
        private readonly IStreamFileUploadService _stremFileUpload;
        private readonly AppDbContext _context;

        public ProductController(IBufferFileUploadSingleApproachService singleBufferUpload, AppDbContext context, IBufferFileUploadMultipleApproachService multipleBufferUpload, IStreamFileUploadService stremFileUpload)
        {
            _singleBufferUpload = singleBufferUpload;
            _context = context;
            _multipleBufferUpload = multipleBufferUpload;
            _stremFileUpload = stremFileUpload;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult SingeİmageAddBufferedGet()
        {

            var data = _context.SingleImageProducts.ToList();
            ViewBag.data = data;
            return View();


        }

        [HttpPost]
        public async Task<IActionResult> SingeİmageAddBuffered(SingleImageProductViewModel vm)
        {

            var path = Guid.NewGuid() + Path.GetExtension(vm.Image?.FileName);
            var result = await _singleBufferUpload.UploadFile(vm.Image!, path);

            if (result)
            {
                _context.SingleImageProducts.Add(new SingleImageProduct()
                {
                    Name = vm.Name,
                    ImagePath = path
                });
                _context.SaveChanges();
                TempData["status"] = "image upload successful";
            }


            return RedirectToAction(nameof(SingeİmageAddBufferedGet));
        }


        public IActionResult MultipleİmageAddBufferedGet()
        {
            var data = _context.MultipleImageProducts.Include(x => x.MultipleImageProductPaths).ToList();
            ViewBag.data = data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MultipleİmageAddBufferedGet(MultipleImageProductViewModel vm)
        {


            var result = await _multipleBufferUpload.UploadFileMultipleApproachAsync(vm.Images);


            if (result != null)
            {



                var multipleImageProduct = new MultipleImageProduct()
                {
                    Name = vm.Name,
                };
                var allPath = result.Select(x => new MultipleImageProductPath()
                {
                    Path = x
                }).ToList();

                multipleImageProduct.MultipleImageProductPaths = allPath;

                _context.MultipleImageProducts.Add(multipleImageProduct);
                _context.SaveChanges();
                TempData["status"] = "image upload successful";

            };





            return RedirectToAction(nameof(MultipleİmageAddBufferedGet));

        }


        public IActionResult StreamFileUploadData()
        {

            ViewBag.data = _context.StreamUploadImageProducts.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult StreamFileUploadData(StreamUploadImageProductViewModel vm)
        {

            StreamUploadImageProduct product = new StreamUploadImageProduct()
            {

                Name = vm.Name,
                Description = vm.Description
            };

            _context.StreamUploadImageProducts.Add(product);
            _context.SaveChanges();

            TempData["id"] = product.Id;


            return RedirectToAction("StreamFileUpload");
        }


        public IActionResult StreamFileUpload()
        {
            TempData["_id"] = TempData["id"];

            return View();
        }


        // request size ayarlamak için bunu program.cs tarafında global olarak yaptım
        //[RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        //[DisableRequestSizeLimit]
        //[Consumes("multipart/form-data")] // for Zip files with form data

        [HttpPost]
        public async Task<IActionResult> StreamFileUploadP()
        {


          



            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
        
            
            /*
             

             content type'dan boundery değerini ayıklıyoruız
                ---boundery  => mime tpye parameter'dır  boundary değer => multipart/form-data isteklerdeki bölümlerin ayırıcısıdır
             HeaderUtilities.RemoveQuotes => tırnak işaretlerini kaldırır

            
             
             */


            var reader = new MultipartReader(boundary, Request.Body);

            var section = await reader.ReadNextSectionAsync();




            var result = await _stremFileUpload.UploadFile(reader, section);

            if (result != null)
            {

                var id = int.Parse(TempData["_id"].ToString());

                var product = _context.StreamUploadImageProducts.Find(id);

                product.ImagePath = result;

                _context.SaveChanges();

            }

            return RedirectToAction("StreamFileUploadData");
        }

        public IActionResult ExplicitModelBind()
        {


            
            ViewBag.data = _context.productStreamApproachV2s.ToList();

            return   View();
        }



        [HttpPost]
        public async Task<IActionResult> ExplicitModelBindPost()
        {

            var mediatypeheadervalue = MediaTypeHeaderValue.Parse(Request.ContentType);

            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
           
            var reader = new MultipartReader(boundary, Request.Body);
            var section = await reader.ReadNextSectionAsync();


           var (path, value)=  await _stremFileUpload.UploadFileWithExplicitBinding(reader, section,this);

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }


            _context.productStreamApproachV2s.Add(new()
            {
                Description = value.Description,
                Name = value.Name,
                İmagePath = path

            });
            _context.SaveChanges();

            return RedirectToAction("ExplicitModelBind");
        }





    }
}
