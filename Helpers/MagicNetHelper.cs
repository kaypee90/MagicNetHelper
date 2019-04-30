namespace CsharpImageCompression.Helpers
{
    using System;
    using System.Drawing;
    using System.IO;
    using ImageMagick;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// MagicNET Image Compression Helper
    /// </summary>
    public class MagicNetHelper
    {
        /// <summary>
        /// Compresses supported image file formats
        /// </summary>
        /// <param name="formFile">Uncompressed Image</param>
        /// <returns>Compressed Image Byte Array</returns>
        public byte[] Compress(IFormFile formFile)
        {
            byte[] imageBytes;
            // Read a file and resize it.
            using(var stream = new MemoryStream())
            {
                formFile.CopyTo(stream);
                imageBytes = stream.ToArray();
            }

            return Compress(uncompressedBytes: imageBytes);
        }

        /// <summary>
        /// Compresses supported image file formats
        /// </summary>
        /// <param name="uncompressedBytes">Uncompressed Image byte array</param>
        /// <returns>Compressed Image Byte Array</returns>

        public byte[] Compress(byte[] uncompressedBytes)
        {
            Console.WriteLine($"Magic Net Initial Filesize: {uncompressedBytes.Length}");
            using (var ms = new MemoryStream(uncompressedBytes))
            {
                ms.Seek(0, SeekOrigin.Begin);

                var optimizer = new ImageOptimizer();
                if(optimizer.IsSupported(ms) && optimizer.Compress(ms))
                {
                    optimizer.LosslessCompress(ms);
                    var compressedBytes = ms.ToArray();
                    Console.WriteLine($"Magic net Compressed Filesize: {compressedBytes.Length}");
                    return compressedBytes;
                }    

               Console.WriteLine("Compression is not supported for this file");
               return uncompressedBytes;
            }
        }

        /// <summary>
        /// Resize images of different file formats
        /// </summary>
        /// <param name="imageBytes">Original Image byte array</param>
        /// <returns>Resized Image Byte Array</returns>

        public byte[] Resize(byte[] imageBytes, int width, int height = 0)
        {
            var image = new MagickImage(imageBytes);
            image.Resize(width, height);
            return image.ToByteArray();
        }

        /// <summary>
        /// Save image from bytes to filesytem
        /// </summary>
        /// <param name="imageBytes">Image byte array</param>
        /// <param name="location">location on filesytem to save file</param>
        /// <returns>A boolean whether the processs got completed</returns>
        public bool SaveImageFile(byte[] imageBytes, string location)
        {
            var hasCompletedSaving = false;
            using(MemoryStream imageStream = new MemoryStream(imageBytes))
            {
                Image imgSave = Image.FromStream(imageStream);
                Bitmap bmSave = new Bitmap(imgSave);
                Bitmap bmTemp = new Bitmap(bmSave);

                Graphics grSave = Graphics.FromImage(bmTemp);
                grSave.DrawImage(imgSave, 0, 0, imgSave.Width, imgSave.Height);

                bmTemp.Save(location);

                imgSave.Dispose();
                bmSave.Dispose();
                bmTemp.Dispose();
                grSave.Dispose();

                hasCompletedSaving = true;
            }
            return hasCompletedSaving;
        }
      
    }
}
