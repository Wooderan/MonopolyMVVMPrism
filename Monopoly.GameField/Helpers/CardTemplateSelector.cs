using Monopoly.GameField.ViewModels;
using Monopoly.Model.Abstract;
using Monopoly.Model.Models;
using Monopoly.Model.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using static Monopoly.Model.Models.TownCard;

namespace Monopoly.GameField.Helpers
{
    class CardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TownCardTemplate { get; set; }
        public DataTemplate ActionCardTemplate { get; set; }
        public DataTemplate MockCardTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is CardViewModel)
            {
                AbstractCard card = (item as CardViewModel).Card;
                if (card != null)
                {
                    if (card is TownCard)
                    {
                        TownCard townCard = card as TownCard;
                        return this.TownCardTemplate;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return this.MockCardTemplate;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
