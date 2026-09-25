using Microsoft.AspNetCore.Http;

namespace Shared.Domain.Common
{
    public static class FileExtension
    {
        static readonly HashSet<string> AllowedMimeTypes = new()
        {
            "application/pdf",  // PDF
            "application/msword",  // DOC
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",  // DOCX
            "application/vnd.ms-excel",  // XLS
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",  // XLSX

            "video/mp4",
            "video/x-msvideo",     // AVI
            "video/x-matroska",    // MKV
            "video/quicktime",     // MOV
            "video/webm",
            "video/3gpp",
            "video/ogg",
            "application/vnd.apple.mpegurl", // HLS
            "video/x-flv",          // FLV

            "image/jpeg",  // JPG, JPEG
            "image/png",
            "image/gif",
            "image/webp",
            "image/svg+xml",
            "image/x-icon" // ICO
        };

        public static bool IsValidFile(IFormFile file)
        {
            return AllowedMimeTypes.Contains(file.ContentType);
        }
    }
}
