using Microsoft.AspNetCore.WebUtilities;

namespace AspNetCore.FileUpload.Approaches.Services.Abstract
{
    public interface IBufferFileUploadSingleApproachService
    {
        Task<bool> UploadFile(IFormFile file,string guildNameWithExtention);
    }

}
