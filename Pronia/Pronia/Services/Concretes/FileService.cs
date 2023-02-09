using System;
using Pronia.Contracts.File;
using Pronia.Services.Abstracts;

namespace Pronia.Services.Concretes
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService>? _logger;

        public FileService(ILogger<FileService>? logger)
        {
            _logger = logger;
        }

        public async Task<string> UploadAsync(IFormFile formFile, UploadDirectory uploadDirectory)
        {
            string directoryPath = GetUploadDirectory(uploadDirectory);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var imageNameInSystem = GenerateUniqueFileName(formFile.FileName);
            var filePath = Path.Combine(directoryPath, imageNameInSystem);

            try
            {
                using FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);
            }
            catch (Exception e)
            {
                _logger!.LogError(e, "Error occured in file service");

                throw;
            }

            return imageNameInSystem;
        }

        public async Task DeleteAsync(string? fileName, UploadDirectory uploadDirectory)
        {
            var deletePath = Path.Combine(GetUploadDirectory(uploadDirectory), fileName);

            await Task.Run(() => File.Delete(deletePath));
        }

        private string GetUploadDirectory(UploadDirectory uploadDirectory)
        {
            string startPath = Path.Combine("wwwroot", "client", "custom-files");

            switch (uploadDirectory)
            {
                case UploadDirectory.Product:
                    return Path.Combine(startPath, "products");
                case UploadDirectory.Slider:
                    return Path.Combine(startPath, "sliders");
                case UploadDirectory.PaymentBenefit:
                    return Path.Combine(startPath, "paymentbenefits");
                case UploadDirectory.Feedback:
                    return Path.Combine(startPath, "feedbacks");
                case UploadDirectory.Blog:
                    return Path.Combine(startPath, "blogs");
                default:
                    throw new Exception("Something went wrong");
            }
        }

        private string GenerateUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }

        public string GetFileUrl(string? fileName, UploadDirectory uploadDirectory)
        {
            string initialSegment = "client/custom-files/";

            switch (uploadDirectory)
            {
                case UploadDirectory.Product:
                    return $"{initialSegment}/products/{fileName}";
                case UploadDirectory.Slider:
                    return $"{initialSegment}/sliders/{fileName}";
                case UploadDirectory.PaymentBenefit:
                    return $"{initialSegment}/paymentbenefits/{fileName}";
                case UploadDirectory.Feedback:
                    return $"{initialSegment}/feedbacks/{fileName}";
                case UploadDirectory.Blog:
                    return $"{initialSegment}/blogs/{fileName}";
                default:
                    throw new Exception("Something went wrong");
            }
        }
    }
}