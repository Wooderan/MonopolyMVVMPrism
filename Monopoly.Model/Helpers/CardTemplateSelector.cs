using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Monopoly.Model.Models;
using Monopoly.Model.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using static Monopoly.Model.Models.TownCard;

namespace Monopoly.Model.Helpers
{
    public class CardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TownCardTemplate { get; set; }
        public DataTemplate StationCardTemplate { get; set; }
        public DataTemplate EventCardTemplate { get; set; }
        public DataTemplate MockCardTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is CardViewModel)
            {
                //AbstractCard card = (item as CardViewModel).Card;
                if ((item as CardViewModel).Card is AbstractCard card)
                {
                    switch (card.Type)
                    {
                        case CardType.TOWN:
                            return this.TownCardTemplate;
                        case CardType.STATION:
                            return this.StationCardTemplate;
                        case CardType.EVENT:
                            return this.EventCardTemplate;
                        default:
                            throw new Exception("Unsupported CardType!");
                    }
                }
                else if ((item as CardViewModel).Card is EventCard eCard)
                {
                    return this.EventCardTemplate;
                }
                else
                {
                    return this.MockCardTemplate;
                }
            }
            else if (item is AbstractCard card)
            {
                switch (card.Type)
                {
                    case CardType.TOWN:
                        return this.TownCardTemplate;
                    case CardType.STATION:
                        return this.StationCardTemplate;
                    case CardType.EVENT:
                        return this.EventCardTemplate;
                    default:
                        throw new Exception("Unsupported CardType!");
                }
            }
            else
            {
                return this.MockCardTemplate;
            }
        }
    }
}
