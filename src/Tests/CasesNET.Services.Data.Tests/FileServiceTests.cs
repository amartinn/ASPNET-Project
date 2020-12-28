namespace CasesNET.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Moq;
    using Xunit;

    public class FileServiceTests
    {
        private readonly string path = @"www\rootpath";
        private readonly string fileName = "test.jpg";

        [Fact]
        public void DeleteImageFromDiscMethodShouldDeleteTheImage()
        {
            // Arrange
            this.CreateCustomFiles(this.fileName);
            var service = new FileService();

            // Act
            service.DeleteImageFromDisc(this.path, "test", "jpg");

            // Assert
            Assert.True(this.IsDirectoryEmpty());
        }

        [Fact]
        public async Task SaveImageToDiskAsyncMethodShouldCreateImage()
        {

            // Arrange
            var service = new FileService();

            // Act
            var formFile = this.CreateTestFormFile(this.fileName, "big content");

            await service.SaveImageToDiskAsync(formFile, this.path);

            // Assert
            Assert.False(this.IsDirectoryEmpty());
        }

        private bool IsDirectoryEmpty()
            => !Directory.EnumerateFileSystemEntries(this.path).Any();

        private void CreateCustomFiles(string fileName)
        {
            var filePath = Path.Combine(this.path, fileName);
            if (!Directory.Exists(this.path))
            {
                Directory.CreateDirectory(this.path);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            file.Close();
        }

        private IFormFile CreateTestFormFile(string name, string content)
        {
            byte[] s_Bytes = Encoding.UTF8.GetBytes(content);

            return new FormFile(
                baseStream: new MemoryStream(s_Bytes),
                baseStreamOffset: 0,
                length: s_Bytes.Length,
                name: "Data",
                fileName: name
            );
        }
    }
}
