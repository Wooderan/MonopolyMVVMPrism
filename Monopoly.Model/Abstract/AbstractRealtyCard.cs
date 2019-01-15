using Monopoly.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Model.Abstract
{
    public abstract class AbstractRealtyCard : AbstractCard
    {
        public enum CardOrientation
        {
            LEFT,
            TOP,
            RIGHT,
            BOTTOM
        }

        #region Properties

        public abstract int CurrentTax
        {
            get;
        }

        #endregion

        #region Fields

        private CardOrientation _orientation;
        public CardOrientation Orientation { get => _orientation; set => _orientation = value; }

        protected int _buyingCost;
        public int Cost { get => _buyingCost; protected set => _buyingCost = value; }

        protected int _pledgeCost;
        public int PledgeCost { get => _pledgeCost; protected set => _pledgeCost = value; }

        protected ICardGroup _cardGroup;
        public ICardGroup CardGroup { get => _cardGroup; protected set => _cardGroup = value; }

        protected ITaxGroup _taxGroup;
        public ITaxGroup TaxGroup { get => _taxGroup; protected set => _taxGroup = value; }

        protected AbstractPlayer _owner;
        public AbstractPlayer Owner { get => _owner; set => this.SetProperty(ref _owner, value); }

        private bool _isPleged;
        public bool IsPleged { get => _isPleged; set => this.SetProperty(ref _isPleged, value); }
        #endregion
    }
}
