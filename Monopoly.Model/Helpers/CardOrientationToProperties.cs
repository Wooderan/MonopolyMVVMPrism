using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static Monopoly.Model.Abstract.AbstractCard;
using static Monopoly.Model.Models.TownCard;

namespace Monopoly.Model.Helpers
{
    public class CardOrientationToYTranslateConverter : IValueConverter
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
                        return null;
                    case CardOrientation.TOP:
                        return 20;
                    case CardOrientation.RIGHT:
                        return null;
                    case CardOrientation.BOTTOM:
                        return -20;
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

    public class CardOrientationToRows : IValueConverter
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
                        return null;
                    case CardOrientation.TOP:
                        return 1;
                    case CardOrientation.RIGHT:
                        return null;
                    case CardOrientation.BOTTOM:
                        return 1;
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

    public class CardOrientationToColumns : IValueConverter
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
                        return 1;
                    case CardOrientation.TOP:
                        return null;
                    case CardOrientation.RIGHT:
                        return 1;
                    case CardOrientation.BOTTOM:
                        return null;
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

    public class CardOrientationToXTranslateConverter : IValueConverter
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
                        return 20;
                    case CardOrientation.TOP:
                        return null;
                    case CardOrientation.RIGHT:
                        return -20;
                    case CardOrientation.BOTTOM:
                        return null;
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

    public class CardOrientationToVerticalAlignment : IValueConverter
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
                        return null;
                    case CardOrientation.TOP:
                        return "Bottom";
                    case CardOrientation.RIGHT:
                        return null;
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

    public class CardOrientationToLabelWidth : IValueConverter
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
                        return 16;
                    case CardOrientation.TOP:
                        return null;
                    case CardOrientation.RIGHT:
                        return 16;
                    case CardOrientation.BOTTOM:
                        return null;
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

    public class CardOrientationToLabelHeight : IValueConverter
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
                        return null;
                    case CardOrientation.TOP:
                        return 16;
                    case CardOrientation.RIGHT:
                        return null;
                    case CardOrientation.BOTTOM:
                        return 16;
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

    public class CardOrientationToHorizontalAlignment : IValueConverter
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
                        return null;
                    case CardOrientation.RIGHT:
                        return "Left";
                    case CardOrientation.BOTTOM:
                        return null;
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
