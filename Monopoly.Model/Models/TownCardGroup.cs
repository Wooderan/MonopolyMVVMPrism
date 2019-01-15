using Monopoly.Model.Interfaces;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using Monopoly.Model.Abstract;

namespace Monopoly.Model.Models
{
    public class TownCardGroup : ICardGroup
    {
        public List<AbstractRealtyCard> Cards { get; set; } = new List<AbstractRealtyCard>();
        public Color GroupColor { get; set; }

        public bool IsMonopoly
        {
            get
            {
                if (this.Cards.All(ac => !ac.IsPleged) && this.Cards.Count > 0)
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

        public TownCardGroup(Color groupColor)
        {
            GroupColor = groupColor;
        }

        public void AddCard(AbstractRealtyCard card)
        {
            this.Cards.Add(card);
        }
    }
}
