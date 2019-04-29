namespace CsharpImageCompression.Tests
{
    using System;
    using CsharpImageCompression.Helpers;
    using Xunit;
    public class MagicHelperTest
    {
        private byte[] OriginalImageBytes {get;}

        public MagicHelperTest()
        {
            OriginalImageBytes = Convert.FromBase64String(
                 "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAAEnQAABJ0Ad5mH3gAAAANSURBVBhXY/h57fF/AAkwA7IFNFSkAAAAAElFTkSuQmCC");
        }

        [Fact]
        public void TestCompressWithBytes()
        {
            // Arrange
            var magicNetHelper = new MagicNetHelper();   

            // Act
            var compressedBytes = magicNetHelper.Compress(OriginalImageBytes);
            
            // Assert
            Assert.True(compressedBytes != null);
            Assert.True(compressedBytes.Length <=  OriginalImageBytes.Length);
        }

        [Fact]
        public void TestResizeWithBytes()
        {
            // Arrange
            var magicNetHelper = new MagicNetHelper();   

            // Act
            var compressedBytes = magicNetHelper.Resize(imageBytes: OriginalImageBytes, width: 765);
            
            // Assert
            Assert.True(compressedBytes != null);
        }

        [Fact]
        public void TestSaveImageFile()
        {
            // Arrange
            var magicNetHelper = new MagicNetHelper(); 
            const string location= "C:\\Projects\\CsharpImageCompression\\OutputImages\\testimage.png";  

            // Act
            var result = magicNetHelper.SaveImageFile(imageBytes: OriginalImageBytes, location: location);
            
            // Assert
            Assert.True(result);
        }
    }
}