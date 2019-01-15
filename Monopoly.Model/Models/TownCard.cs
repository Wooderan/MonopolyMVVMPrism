using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using System;

namespace Monopoly.Model.Models
{


    public class TownCard : AbstractRealtyCard
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

        #region Methods

        internal void AddHouse()
        {
            if (this.Houses < 5)
            {
                this.Houses++;
            }
        }

        internal void RemoveHouse()
        {
            if (this.Houses > 0)
            {
                this.Houses--;
            }
        }

        #endregion

        #region Properties

        public override int CurrentTax
        {
            get
            {
                if (this.IsPleged)
                {
                    return 0;
                }
                else
                {
                    switch (this.Houses)
                    {
                        case 0:
                            return this.TaxGroup.Empty;
                        case 1:
                            return this.TaxGroup.House;
                        case 2:
                            return this.TaxGroup.TwoHouses;
                        case 3:
                            return this.TaxGroup.ThreeHouses;
                        case 4:
                            return this.TaxGroup.FourHouses;
                        case 5:
                            return this.TaxGroup.Hotel;
                        default:
                            throw new Exception("Unaccepted count of houses! Can't return CurrentTax!");
                    }
                }
            }
        }

        #endregion

        #region Fields

        protected int _houseCost;
        public int HouseCost { get => _houseCost; protected set => _houseCost = value; }

        protected int _houses;
        public int Houses { get => _houses; set => this.SetProperty(ref _houses, value); }

        #endregion

    }
}
