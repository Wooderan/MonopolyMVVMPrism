using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Monopoly.Model.Images
{
    public static class DefaultImagesLocator
    {
        public static string GetDefaultAvatar()
        {
            return "pack://application:,,,/Monopoly.Model;component/Images/Avatars/man-1.png";
        }
        public static string GetAvatar(string avatarName)
        {
            return $"pack://application:,,,/Monopoly.Model;component/Images/Avatars/{avatarName}";
        }

        public static string GetDefaultChip()
        {
            return "pack://application:,,,/Monopoly.Model;component/Images/Chips/coffe-cup-aqua.png";
        }
        public static string GetChip(string chipName)
        {
            return $"pack://application:,,,/Monopoly.Model;component/Images/Chips/{chipName}";
        }

        public static string GetDefaultEventPicture()
        {
            return "pack://application:,,,/Monopoly.Model;component/Images/Events/default.png";
        }

        public static string GetEventPicture(string eventName)
        {
            return $"pack://application:,,,/Monopoly.Model;component/Images/Events/{eventName}";
        }
    }
}
