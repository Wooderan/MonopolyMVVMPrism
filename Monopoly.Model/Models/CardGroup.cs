using Monopoly.Model.Interfaces;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using Monopoly.Model.Abstract;

namespace Monopoly.Model.Models
{
    public class CardGroup : ICardGroup
    {
        public List<AbstractCard> Cards { get; set; } = new List<AbstractCard>();
        public Color GroupColor { get; set; }

        public bool IsMonopoly
        {
            get
            {
                if ( this.Cards.Count > 0)
                {
                    var first = this.Cards[0].Owner;
                    return this.Cards.Select(c => c.Owner).All(ap => ap != null && ap == first);
                }
                else
                {
                    return false;
                }
            }
        }

        public CardGroup(Color groupColor)
        {
            GroupColor = groupColor;
        }

        public void AddCard(AbstractCard card)
        {
            this.Cards.Add(card);
        }
    }
}
