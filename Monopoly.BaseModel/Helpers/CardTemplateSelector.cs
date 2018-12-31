using Monopoly.BaseModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Monopoly.BaseModel.Helpers
{
    class CardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LeftTownCard { get; set; }
        public DataTemplate TopTownCard { get; set; }
        public DataTemplate RightTownCard { get; set; }
        public DataTemplate BottomTownCard { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TownCard)
            {
                TownCard card = item as TownCard;

                switch (card.CardOrientation)
                {
                    case CardOrientation.LEFT:
                        return this.LeftTownCard;
                    case CardOrientation.TOP:
                        return this.TopTownCard;
                    case CardOrientation.RIGHT:
                        return this.RightTownCard;
                    case CardOrientation.BOTTOM:
                        return this.BottomTownCard;
                    default:
                        throw new Exception("Unavailable CardOrientation!");
                }
            }
            else
            {
                return null;
            }
        }
    }
}
