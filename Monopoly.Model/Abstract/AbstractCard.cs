using Monopoly.Model.Interfaces;
using Prism.Mvvm;
using System;

namespace Monopoly.Model.Abstract
{

    public abstract class AbstractCard : BindableBase, ICard
    {
        #region Constructors

        #endregion

        #region Methods

        public void AddHouse()
        {
            if (this.Houses < 5)
            {
                this.Houses++;
            }
        }

        public void RemoveHouse()
        {
            if (this.Houses > 0)
            {
                this.Houses--;
            }
        }
        #endregion

        #region Properties

        public int CurrentTax
        {
            get
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


        #endregion

        #region Fields

        protected CardType _type;
        protected string _name;
        protected int _buyingCost;
        protected int _pledgeCost;
        protected int _houseCost;
        protected ICardGroup _cardGroup;
        protected ITaxGroup _taxGroup;
        protected int _houses;
        protected AbstractPlayer _owner;


        public CardType Type { get => _type; protected set => _type = value; }
        public string Name { get => _name; protected set => _name = value; }
        public int Cost { get => _buyingCost; protected set => _buyingCost = value; }
        public int PledgeCost { get => _pledgeCost; protected set => _pledgeCost = value; }
        public int HouseCost { get => _houseCost; protected set => _houseCost = value; }
        public ICardGroup CardGroup { get => _cardGroup; protected set => _cardGroup = value; }
        public ITaxGroup TaxGroup { get => _taxGroup; protected set => _taxGroup = value; }

        //bindable
        public int Houses { get => _houses; set => this.SetProperty(ref _houses, value); }
        public AbstractPlayer Owner { get => _owner; set => this.SetProperty(ref _owner, value); }

        #endregion // Fields
    }
}
