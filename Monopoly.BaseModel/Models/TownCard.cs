﻿using Monopoly.BaseModel.Models.Interfaces;
using Monopoly.Infrastructure.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.BaseModel.Models
{
    

    public class TownCard : AbstractCard
    {
        public enum CardOrientation
        {
            LEFT,
            TOP,
            RIGHT,
            BOTTOM
        }

        public TownCard()
        {

        }

        private CardOrientation _orientation;
        public CardOrientation Orientation { get => _orientation; set => _orientation = value; }

        public TownCard(string name, int Cost, int pledgeCost, int houseCost, CardGroup cardGroup, TaxGroup taxGroup, CardOrientation orientation)
        {
            this.Type = CardType.TOWN;
            this.Name = name;
            this.Cost = Cost;
            this.PledgeCost = pledgeCost;
            this.HouseCost = houseCost;
            this.CardGroup = cardGroup;
            this.TaxGroup = taxGroup;
            this.Orientation = orientation;
        }

    }
}
