using Monopoly.GameField.ViewModels;
using Monopoly.Model.Abstract;
using Monopoly.Model.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using static Monopoly.Model.Models.TownCard;

namespace Monopoly.GameField.Helpers
{
    class CardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LeftTownCard { get; set; }
        public DataTemplate TopTownCard { get; set; }
        public DataTemplate RightTownCard { get; set; }
        public DataTemplate BottomTownCard { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is CardViewModel)
            {
                AbstractCard card = (item as CardViewModel).Card;

                if (card is TownCard)
                {
                    TownCard townCard = card as TownCard;

                    switch (townCard.Orientation)
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
            else
            {
                return null;
            }
        }
    }
}
