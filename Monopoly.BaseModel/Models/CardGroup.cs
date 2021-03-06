﻿using Monopoly.BaseModel.Models.Interfaces;
using Monopoly.Infrastructure.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Monopoly.BaseModel.Models
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
