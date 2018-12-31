using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Monopoly.BaseModel.Images
{
    public static class DefaultImagesLocator
    {
        public static BitmapImage GetDefaultAvatar()
        {
            return new BitmapImage(new Uri("Avatars/man-1.png", UriKind.Relative));
        }

        public static BitmapImage GetDefaultChip()
        {
            return new BitmapImage(new Uri("Chips/coffe-cup-aqua.png", UriKind.Relative));
        }
    }
}
