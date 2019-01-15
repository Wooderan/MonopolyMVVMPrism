using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Monopoly.Model.Models
{
    public class StationCardGroup : ICardGroup
    {
        public List<AbstractRealtyCard> Cards { get; set; } = new List<AbstractRealtyCard>();
        public Color GroupColor { get; set; }

        public bool IsMonopoly => false;

        public void AddCard(AbstractRealtyCard card)
        {
            this.Cards.Add(card);
        }

        public StationCardGroup()
        {
        }

        public int MonopolyLevel(AbstractPlayer player)
        {
            return this.Cards.Where(ac => ac.Owner == player).Count();
        }
    }
}
