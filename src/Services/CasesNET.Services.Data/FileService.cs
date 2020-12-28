namespace CasesNET.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class FileService : IFileService
    {
        public void DeleteImageFromDisc(string path, string imageName, string imageExtension)
        {
            var imagePath = Path.Combine(path, $"{imageName}.{imageExtension}");
            File.Delete(imagePath);
        }

        public async Task SaveImageToDiskAsync(IFormFile file, string path)
        {
            string filePath = Path.Combine(path, file.FileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}
