using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static Monopoly.Model.Models.TownCard;

namespace Monopoly.Model.Helpers
{
    public class CardOrientationToDockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (value is CardOrientation)
            {
                switch ((CardOrientation)value)
                {
                    case CardOrientation.LEFT:
                        return "Right";
                    case CardOrientation.TOP:
                        return "Bottom";
                    case CardOrientation.RIGHT:
                        return "Left";
                    case CardOrientation.BOTTOM:
                        return "Top";
                    default:
                        throw new Exception("Unsupported card orientation");
                }
            }
            else
            {
                Type type = value.GetType();
                throw new InvalidOperationException("Unsuppoted type [" + type.Name + "]");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
