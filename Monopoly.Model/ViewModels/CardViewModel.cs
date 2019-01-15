using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Monopoly.Model.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Media;
using static Monopoly.Model.Abstract.AbstractCard;
using static Monopoly.Model.Abstract.AbstractRealtyCard;
using static Monopoly.Model.Models.TownCard;

namespace Monopoly.Model.ViewModels
{
    public class CardViewModel : BindableBase
    {

        #region Constructors

        //public CardViewModel()
        //{
        //    this.Card = new TownCard("Livia", 50, 25, 50, new CardGroup(Colors.Blue), new TaxGroup(5, 10, 18, 30, 90, 160, 250), CardOrientation.BOTTOM);
        //    this.RaisePropertyChanged("Card");
        //    if (this.Card != null)
        //    {
        //        this.Card.PropertyChanged += (s, e) =>
        //        {
        //            this.RaisePropertyChanged(e.PropertyName);
        //        };
        //    }
        //}

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
            _gameManager = manager;
        }

        #endregion

        #region Commands

        private DelegateCommand _clickCommand;
        public DelegateCommand ClickCommand =>
            _clickCommand ?? (_clickCommand = new DelegateCommand(ExecuteClickCommand));

        void ExecuteClickCommand()
        {
            this.onClickAction?.Invoke();
        }

        public Action onClickAction { get; set; }

        #endregion

        #region Fields

        private AbstractCard _card;
        public AbstractCard Card
        {
            get { return _card; }
            set { SetProperty(ref _card, value); }
        }

        private IGameManager _gameManager;

        private bool _gray;
        public bool Gray
        {
            get { return _gray; }
            set { SetProperty(ref _gray, value); }
        }



        //inner properties
        public CardType Type => this.Card.Type;
        public string Name => this.Card.Name;
        public int Cost => (this.Card as AbstractRealtyCard)?.Cost ?? 0;
        public int PledgeCost => (this.Card as AbstractRealtyCard)?.PledgeCost ?? 0;
        public int HouseCost => (this.Card as TownCard)?.HouseCost ?? 0;
        public ICardGroup CardGroup => (this.Card as AbstractRealtyCard)?.CardGroup ?? null;
        public ITaxGroup TaxGroup => (this.Card as AbstractRealtyCard)?.TaxGroup ?? null;
        public int Houses => (this.Card as TownCard)?.Houses ?? 0;
        public AbstractPlayer Owner => (this.Card as AbstractRealtyCard)?.Owner ?? null;
        public bool IsPleged => (this.Card as AbstractRealtyCard)?.IsPleged ?? false;
        public CardOrientation Orientation => (this.Card as AbstractRealtyCard)?.Orientation ?? CardOrientation.RIGHT;
        public string EventPicture => (this.Card as EventCard)?.EventPicture ?? string.Empty;

        #endregion
    }
}
