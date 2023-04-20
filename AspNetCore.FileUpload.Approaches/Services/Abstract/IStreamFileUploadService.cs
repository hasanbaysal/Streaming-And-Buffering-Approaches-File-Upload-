using AspNetCore.FileUpload.Approaches.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace AspNetCore.FileUpload.Approaches.Services.Abstract
{
    public interface IStreamFileUploadService
    {
        Task<string?> UploadFile(MultipartReader reader, MultipartSection section);
        Task<(string?, ProductStreamApproachV2ViewModel)> UploadFileWithExplicitBinding(MultipartReader reader, MultipartSection section, ControllerBase controller);
    }
}

