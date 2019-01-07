using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Monopoly.Model.Models;
using Prism.Commands;
using Prism.Mvvm;
using static Monopoly.Model.Models.TownCard;

namespace Monopoly.Model.ViewModels
{
    public class CardViewModel : BindableBase
    {
                
        #region Constructors

        public CardViewModel(AbstractCard card, IGameManager manager)
        {
            this.Card = card;
            this.RaisePropertyChanged("Card");
            if (this.Card != null)
            {
                this.Card.PropertyChanged += (s, e) =>
                {
                    this.RaisePropertyChanged(e.PropertyName);
                };
            }
        }

        #endregion

        #region Commands

        public DelegateCommand DefaultCommand { get; }

        #endregion

        #region Fields

        private AbstractCard _card;
        public AbstractCard Card
        {
            get { return _card; }
            set { SetProperty(ref _card, value); }
        }

        //inner properties
        public CardType Type => this.Card.Type;
        public string Name => this.Card.Name;
        public int Cost => this.Card.Cost;
        public int PledgeCost => this.Card.PledgeCost;
        public int HouseCost => this.Card.HouseCost;
        public ICardGroup CardGroup => this.Card.CardGroup;
        public ITaxGroup TaxGroup => this.Card.TaxGroup;
        public int Houses => this.Card.Houses;
        public AbstractPlayer Owner => this.Card.Owner;
        public CardOrientation Orientation => (this.Card is TownCard) ? (this.Card as TownCard).Orientation : CardOrientation.TOP;

        #endregion
    }
}
