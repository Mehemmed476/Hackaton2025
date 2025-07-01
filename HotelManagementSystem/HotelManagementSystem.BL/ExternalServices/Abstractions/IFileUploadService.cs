using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.BL.ExternalServices.Abstractions;

public interface IFileUploadService
{
    Task<string> SaveFileAsync(IFormFile imageFile, string envPath, string[] allowedFileExtensions);
    void DeleteFile(string fileNameWithExtension, string envPath);
}
