using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Models
{
    public class StationCard : AbstractCard
    {
        public StationCard(string name, int Cost, int pledgeCost, StationCardGroup cardGroup, TaxGroup taxGroup, CardOrientation orientation)
        {
            this.Type = CardType.STATION;
            this.Name = name;
            this.Cost = Cost;
            this.PledgeCost = pledgeCost;
            this.CardGroup = cardGroup;
            this.CardGroup.AddCard(this);
            this.TaxGroup = taxGroup;
            this.Orientation = orientation;
        }

        public override int CurrentTax {
            get
            {
                if (this.IsPleged)
                {
                    return 0;
                }
                else
                {
                    switch ((this.CardGroup as StationCardGroup).MonopolyLevel(this.Owner))
                    {
                        case 1:
                            return this.TaxGroup.Empty;
                        case 2:
                            return this.TaxGroup.House;
                        case 3:
                            return this.TaxGroup.TwoHouses;
                        case 4:
                            return this.TaxGroup.ThreeHouses;
                        default:
                            throw new Exception("Unaccepted count of Stations! Can't return CurrentTax!");
                    }
                }
            }
        }
    }
}
