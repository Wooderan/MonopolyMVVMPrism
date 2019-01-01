using Monopoly.Model.Interfaces;
using System.Collections.Generic;
using System.Windows.Media;

namespace Monopoly.Model.Models
{
    public class CardGroup : ICardGroup
    {
        public List<ICard> Cards { get; set; }
        public Color GroupColor { get; set; }

        public CardGroup(Color groupColor)
        {
            GroupColor = groupColor;
        }

        public void AddCard(ICard card)
        {
            this.Cards.Add(card);
        }
    }
}
