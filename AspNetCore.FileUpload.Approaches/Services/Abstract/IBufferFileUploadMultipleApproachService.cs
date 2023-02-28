namespace AspNetCore.FileUpload.Approaches.Services.Abstract
{
    public interface IBufferFileUploadMultipleApproachService
    {
        Task<List<string>?> UploadFileMultipleApproachAsync(IEnumerable<IFormFile> files);
    }
}
