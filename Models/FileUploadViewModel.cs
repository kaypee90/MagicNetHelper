using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsharpImageCompression.Models
{
    public class FileUploadViewModel
    {
        public IFormFile FormFile { get; set; }
    }
}
