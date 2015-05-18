using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Repository.Model
{
    class Resize
    {
        //http://stackoverflow.com/questions/6501797/resize-image-proportionally-with-maxheight-and-maxwidth-constraints

        public static Image ScaleImage(Image image)
        {
            //Lägg in dessas värden i en settings fil!!
            int maxWidth = 200;
            int maxHeight = 200;

            ImageFormat imageFormat = image.RawFormat;

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

            //Fixa så att namnet blir product id:t.
            newImage.Save(@"c:\test.png", imageFormat);
            return newImage;
        }
    }
}
