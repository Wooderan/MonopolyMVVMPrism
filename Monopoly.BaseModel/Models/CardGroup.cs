using Monopoly.BaseModel.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Monopoly.BaseModel.Models
{
    public class CardGroup
    {
        public List<AbstractCard> Cards { get; set; }
        public Color GroupColor { get; set; }

        public CardGroup(Color groupColor)
        {
            GroupColor = groupColor;
        }

        public void AddCard(AbstractCard card)
        {
            this.Cards.Add(card);
            card.CardGroup = this;
        }
    }
}
