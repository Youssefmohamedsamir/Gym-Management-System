using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace GymManagementBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        public AttachmentService(IWebHostEnvironment webHost)
        {
            _webHostEnvironment = webHost;
        }
        private readonly string[] allowedExtentions = { ".jpg", ".png", ".jpeg" };
        private readonly long maxFileSize = 5 * 1024 * 1024;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                if (folderName is null || file is null || file.Length == 0) return null;
                if (file.Length > maxFileSize) return null;

                var extention = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtentions.Contains(extention)) return null;

                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", folderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = Guid.NewGuid().ToString() + extention;

                var filePath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);

                file.CopyTo(stream);

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Upload File To Folder = {folderName}:{ex}");
                return null;
            }
        }
        public bool Delete(string fileName, string folderName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folderName)) return false;
                var FullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", folderName, fileName);
                if (!File.Exists(FullPath)) return false;
                File.Delete(FullPath);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To Delete File {fileName} From Folder = {folderName}:{ex}");
                return false;
            }

        }
    }
}
