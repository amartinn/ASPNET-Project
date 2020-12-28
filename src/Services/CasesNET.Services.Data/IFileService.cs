namespace CasesNET.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IFileService
    {
        Task SaveImageToDiskAsync(IFormFile file, string path);

        void DeleteImageFromDisc(string path, string imageName, string imageExtension);
    }
}
