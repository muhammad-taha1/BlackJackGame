using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BlackJackGame.CardImgs
{
    public static class ImageParser
    {
        public static Bitmap getImage(int value, int suit)
        {
            Bitmap img = new Bitmap("C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/cards.png");

            // 3 px = space between each card
            // 70 px = width of each card
            // 1 px = start position offset
            // 95 px = height of each card
            Rectangle section = new Rectangle(new Point((3+70)*value + 1, (95+3)*suit + 1), new Size(70, 95));
            Bitmap CroppedImage = CropImage(img, section);
            return CroppedImage;
           // CroppedImage.Save("C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/card1.png");

        }

        public static Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }

    }
}
