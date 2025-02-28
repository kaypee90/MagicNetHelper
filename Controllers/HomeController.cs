﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CsharpImageCompression.Models;
using CsharpImageCompression.Helpers;

namespace CsharpImageCompression.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(FileUploadViewModel viewModel)
        {
            Console.WriteLine(value: $"Content-Disposition: {viewModel.FormFile.ContentDisposition}");

            if(ModelState.IsValid)
            {
                var imageProcessor = new MagicNetHelper();
                var intialCompressedBytes = imageProcessor.Compress(viewModel.FormFile);
                var resizedBytes = imageProcessor.Resize(imageBytes: intialCompressedBytes, width: 765, height: 0);

                const string basePath = "C:\\Projects\\CsharpImageCompression\\OutputImages\\";
                imageProcessor.SaveImageFile(imageBytes: resizedBytes, 
                                            location: basePath + viewModel.FormFile.FileName);
            }

            return View(viewModel);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
