using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace Monopoly.Model.ViewModels
{
    public class CardViewModel : BindableBase
    {
                
        #region Constructors

        public CardViewModel(AbstractCard card, IGameManager manager)
        {
            this.Card = card;
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

        public AbstractCard Card { get; }

        //inner properties
        public CardType Type => this.Card.Type;
        public string Name => this.Card.Name;
        public int Cost => this.Card.Cost;
        public int PledgeCost => this.Card.PledgeCost;
        public int HouseCost => this.Card.HouseCost;
        public ICardGroup CardGroup => this.Card.CardGroup;
        public ITaxGroup TaxGroup => this.Card.TaxGroup;
        public int Houses => this.Card.Houses;
        public IPlayer Owner => this.Card.Owner;

        #endregion
    }
}
