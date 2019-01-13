﻿using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;

namespace Monopoly.Model.Models
{


    public class TownCard : AbstractCard
    {

        public TownCard()
        {

        }

        public TownCard(string name, int Cost, int pledgeCost, int houseCost, TownCardGroup cardGroup, TaxGroup taxGroup, CardOrientation orientation)
        {
            this.Type = CardType.TOWN;
            this.Name = name;
            this.Cost = Cost;
            this.PledgeCost = pledgeCost;
            this.HouseCost = houseCost;
            this.CardGroup = cardGroup;
            this.CardGroup.AddCard(this);
            this.TaxGroup = taxGroup;
            this.Orientation = orientation;
        }

    }
}
