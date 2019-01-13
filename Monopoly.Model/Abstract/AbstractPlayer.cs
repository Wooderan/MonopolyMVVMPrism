using Monopoly.Model.Images;
using Monopoly.Model.Interfaces;
using Prism.Mvvm;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Monopoly.Model.Abstract
{
    public abstract class AbstractPlayer : BindableBase, IPlayer
    {

        #region Constructors
        public AbstractPlayer()
        {
            this.CardPosition = 0;
        }
        #endregion

        #region Properties

        public string Avatar {
            get
            {
                if (_avatar == null)
                {
                    _avatar = DefaultImagesLocator.GetDefaultAvatar();
                }
                return _avatar;
            }
            set => _avatar = value;
        }
        public string Chip {
            get
            {
                if (_chip == null)
                {
                    _chip = DefaultImagesLocator.GetDefaultChip();
                }
                return _chip;
            }
            set => _chip = value;
        }

        #endregion

        #region Events

        public event Action<int> MoneyDecreaseEvent;
        public event Action<int> MoneyIncreaseEvent;

        #endregion

        #region Methods

        internal void GetTax(int currentTax)
        {
            this.Money += currentTax;
            this.MoneyIncreaseEvent?.Invoke(currentTax);
        }

        internal void PayTax(int currentTax)
        {
            this.Money -= currentTax;
            this.MoneyDecreaseEvent?.Invoke(currentTax);
        }

        internal void BuyTown(AbstractCard currentCard)
        {
            if (this.Money > currentCard.Cost)
            {
                this.RealtyCards.Add(currentCard);
                currentCard.Owner = this;
                this.Money -= currentCard.Cost;
                this.MoneyDecreaseEvent?.Invoke(currentCard.Cost);
            }
        }

        internal void BuyTown(AbstractCard currentCard, int cost)
        {
            if (this.Money > cost)
            {
                this.RealtyCards.Add(currentCard);
                currentCard.Owner = this;
                this.Money -= cost;
                this.MoneyDecreaseEvent?.Invoke(cost);
            }
        }

        internal void BuyHouse(AbstractCard card)
        {
            if (this.CheckIfOwnCard(card))
            {
                if (this.Money >= card.HouseCost)
                {
                    card.AddHouse();
                    this.Money -= card.HouseCost;
                    this.MoneyDecreaseEvent?.Invoke(card.HouseCost);
                }

            }
            else
            {
                throw new Exception("Can't build on enemies town!");
            }
        }


        internal void DestroyHouse(AbstractCard card)
        {
            if (this.CheckIfOwnCard(card))
            {
                card.RemoveHouse();
                this.Money += card.HouseCost/2;
                this.MoneyIncreaseEvent?.Invoke(card.HouseCost/2);
            }
            else
            {
                throw new Exception("Can't build on enemies town!");
            }
        }

        private bool CheckIfOwnCard(AbstractCard card)
        {
            if (this.RealtyCards.Where(ac => ac == card).Any())
            {
                return true;

            }
            else
            {
                return false;
            }
        }


        internal void PledgeCard(AbstractCard card)
        {
            if (this.CheckIfOwnCard(card))
            {
                card.IsPleged = true;
                this.Money += card.PledgeCost;
                this.MoneyIncreaseEvent?.Invoke(card.PledgeCost);
            }
            else
            {
                throw new Exception("Can't build on enemies town!");
            }
        }


        internal void BuyFromPledgeCard(AbstractCard card)
        {
            if (this.CheckIfOwnCard(card))
            {
                if (this.Money > card.PledgeCost)
                {
                    card.IsPleged = false;
                    this.Money -= card.PledgeCost;
                    this.MoneyDecreaseEvent?.Invoke(card.PledgeCost);
                }
            }
            else
            {
                throw new Exception("Can't build on enemies town!");
            }
        }

        #endregion

        #region Fields

        protected string _nickname;
        protected string _avatar;
        protected string _chip;
        protected int _money;
        protected bool _isActive;

        private int _cardPosition;

        public int CardPosition
        {
            get
            {
                return _cardPosition;
            }
            internal set
            {
                if (value <= 39)
                {
                    _cardPosition = value;
                }
                else
                {
                    _cardPosition = 0;
                }
                this.RaisePropertyChanged();
            }
        }

        public string Nickname { get => _nickname; set => _nickname = value; }
        public int Money { get => _money; set => this.SetProperty(ref _money, value); }
        public ObservableCollection<AbstractCard> RealtyCards { get; set; } = new ObservableCollection<AbstractCard>();
        public ObservableCollection<AbstractCard> ActionCards { get; set; } = new ObservableCollection<AbstractCard>();

        public bool HaveMoney => this.Money > 0;
        public bool IsActive { get => _isActive; internal set => this.SetProperty(ref _isActive, value); }

        #endregion

    }
}
