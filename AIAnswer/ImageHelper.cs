using System.Drawing;
using System.IO;

namespace AIAnswer
{
    public class ImageHelper
    {
        public static MemoryStream CaptureImage(Stream stream, int offsetX, int offsetY, int width, int height)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using (Image fromImage = Image.FromStream(stream))
            {
                using (var bitMap = new Bitmap(width, height))
                {
                    using (var graphic = Graphics.FromImage(bitMap))
                    {
                        graphic.DrawImage(fromImage, 0, 0, new Rectangle(offsetX, offsetY, width, height), GraphicsUnit.Pixel);
                        var ms = new MemoryStream();
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Tiff);
                        ms.Seek(0, SeekOrigin.Begin);
                        return ms;
                    }
                }
            }
        }
    }
}
