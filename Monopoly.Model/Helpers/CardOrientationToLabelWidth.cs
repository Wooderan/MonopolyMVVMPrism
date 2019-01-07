﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static Monopoly.Model.Models.TownCard;

namespace Monopoly.Model.Helpers
{
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
}
