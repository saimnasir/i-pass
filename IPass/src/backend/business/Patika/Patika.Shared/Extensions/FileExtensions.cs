using ImageMagick;
using Microsoft.AspNetCore.Http;
using Patika.Shared.Consts;
using Patika.Shared.Enums;
using Patika.Shared.Exceptions.FileDomain;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Patika.Shared.Extensions
{
    public static class FileExtensions
    {
        public static MagickFormat GetImageFormat(this string imageExtension) => imageExtension.ToLower() switch
        {
            "png" => MagickFormat.Png,
            "jpg" => MagickFormat.Jpg,
            "jpeg" => MagickFormat.Jpeg,
            "webp" => MagickFormat.WebP,
            // PNG
            _ => MagickFormat.Unknown,
        };
        // ASK: Can we use this for null strings?
        public static string GetUniqueFileName(this string fileName)
        {
            return !string.IsNullOrEmpty(fileName) ? fileName : Guid.NewGuid().ToString();
        }

        public static string GetContainerName(this FileTypes fileType, string size)
        {
            return fileType switch
            {
                FileTypes.Profile => $"{size}-profile",
                FileTypes.Message => $"{size}-message",
                FileTypes.SuggestionCategories => $"{size}-suggestion-categories",
                _ => "Files",
            };
        }

        public static Size GetImageSize(this MagickImage image, int percentage)
        {
            int height = image.Height * percentage / 100;
            int width = image.Width * percentage / 100;
            var point = new Point(width, height);
            var size = new Size(point);
            return size;
        }

        public static async Task<string> ConvertToString(this IFormFile file)
        {
            var mem = new MemoryStream();
            await file.CopyToAsync(mem);
            var base64 = Convert.ToBase64String(mem.ToArray());
            return base64;
        }


        public static string GetSize(this int percentage) => percentage switch
        {
            FileUploadConsts.None => string.Empty,
            FileUploadConsts.SmalImagePercentage => "small",
            FileUploadConsts.MediumImagePercentage => $"medium",
            _ => "large",
        };

        public static string GetExtension(this IFormFile image)
        {
            var ex =   Path.GetExtension(image.FileName);

            return ex.Replace(".", "");
        }

        public static string GetImageExtension(this string imageExtension, MagickImage image)
        {
            image.Format = imageExtension.GetImageFormat();

            if (image.Format == MagickFormat.Unknown)
            {
                imageExtension = "png";
                image.Format = MagickFormat.Png;
            }
            return imageExtension;
        }
        public static void SetImageFormat(this string imageExtension, MagickImage image)
        {
            image.Format = imageExtension.GetImageFormat();

            if (image.Format == MagickFormat.Unknown)
            {
                image.Format = MagickFormat.Png;
            }
        }

        public static async Task<MultipartFormDataContent> ConvertToByteArrayContent(this IFormFile file)
        {
            var content = new MultipartFormDataContent();
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var byteArrayContent = new ByteArrayContent(ms.ToArray());

            content.Add(byteArrayContent, "file", file.FileName); 
            return content;
        }
        public static string ConvertFormFileToBase64(this IFormFile file)
        {
            if (file.Length > 0)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string fileAsBase64 = Convert.ToBase64String(fileBytes);

                return fileAsBase64;
            }
            throw new FileIsNotValidException();
        }
        public static IFormFile ConvertBase64ToFormFile(this string file, string fileName)
        {
            if (file.Length > 0)
            {

                byte[] bytes = Convert.FromBase64String(file);
                MemoryStream stream = new MemoryStream(bytes);

                IFormFile formFile = new FormFile(stream, 0, bytes.Length, fileName, fileName);

                return formFile;
            }
            throw new FileIsNotValidException();
        }
    }
}
