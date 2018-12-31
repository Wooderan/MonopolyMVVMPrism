using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.BaseModel.Models.Interfaces
{

    public enum CardType
    {
        TOWN,
        STATION,
        ACTION,
    }

    public abstract class AbstractCard : BindableBase
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

        private CardType _type;
        private string _name;
        private int _buyingCost;
        private int _pledgeCost;
        private int _houseCost;
        private CardGroup _cardGroup;
        private TaxGroup _taxGroup;
        private int _houses;
        private AbstractPlayer _owner;


        public CardType Type { get => _type; set => _type = value; }
        public string Name { get => _name; set => _name = value; }
        public int Cost { get => _buyingCost; set => _buyingCost = value; }
        public int PledgeCost { get => _pledgeCost; set => _pledgeCost = value; }
        public int HouseCost { get => _houseCost; set => _houseCost = value; }
        public CardGroup CardGroup { get => _cardGroup; set => _cardGroup = value; }
        public TaxGroup TaxGroup { get => _taxGroup; set => _taxGroup = value; }

        //bindable
        public int Houses { get => _houses; set => this.SetProperty(ref _houses, value); }
        public AbstractPlayer Owner { get => _owner; set => this.SetProperty(ref _owner, value); }

        #endregion // Fields
    }
}
