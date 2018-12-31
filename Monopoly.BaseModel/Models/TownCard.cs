using Monopoly.BaseModel.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.BaseModel.Models
{
    public enum CardOrientation
    {
        LEFT,
        TOP,
        RIGHT,
        BOTTOM
    }

    public class TownCard : AbstractCard
    {
        public TownCard()
        {

        }

        private CardOrientation _cardOrientation;
        public CardOrientation CardOrientation { get => _cardOrientation; set => _cardOrientation = value; }

        public TownCard(string name, int Cost, int pledgeCost, int houseCost, CardGroup cardGroup, TaxGroup taxGroup, CardOrientation orientation)
        {
            this.Type = CardType.TOWN;
            this.Name = name;
            this.Cost = Cost;
            this.PledgeCost = pledgeCost;
            this.HouseCost = houseCost;
            this.CardGroup = cardGroup;
            this.TaxGroup = taxGroup;
            this.CardOrientation = orientation;
        }

    }
}
