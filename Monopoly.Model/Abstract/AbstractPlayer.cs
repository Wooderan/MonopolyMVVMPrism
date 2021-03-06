﻿using Monopoly.Model.Images;
using Monopoly.Model.Interfaces;
using Prism.Mvvm;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using Monopoly.Model.Models;

namespace Monopoly.Model.Abstract
{
    public enum PlayerType
    {
        PLAYER,
        COMPUTER
    }
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

        private int _cantAct;
        public int CantAct
        {
            get { return _cantAct; }
            internal set { SetProperty(ref _cantAct, value); }
        }

        #endregion

        #region Events

        public event Action<int,int> MoneyDecreaseEvent;

        public event Action<int,int> MoneyIncreaseEvent;

        #endregion

        #region Methods

        internal void ThrowMoney(int money, int delay)
        {
            this.Money -= money;
            this.MoneyDecreaseEvent?.Invoke(money, delay);
        }

        internal void PickUpMoney(int money, int delay)
        {
            this.Money += money;
            this.MoneyIncreaseEvent?.Invoke(money, delay);
        }

        internal void PickUpCard(AbstractCard acard)
        {
            if (acard is AbstractRealtyCard card)
            {
                if (card.Type == CardType.STATION || card.Type == CardType.TOWN)
                {
                    this.RealtyCards.Add(card);
                    card.Owner = this;
                }
            }
        }

        internal void ThrowCard(AbstractCard acard)
        {
            if (acard is AbstractRealtyCard card)
            {
                if (card.Type == CardType.STATION || card.Type == CardType.TOWN)
                {
                    this.RealtyCards.Remove(card);
                    card.Owner = null;
                }
            }
        }

        internal void GetTax(int currentTax)
        {
            this.Money += currentTax;
            this.MoneyIncreaseEvent?.Invoke(currentTax, 0);
        }

        internal void PayTax(int currentTax)
        {
            this.Money -= currentTax;
            this.MoneyDecreaseEvent?.Invoke(currentTax, 0);
        }

        internal void BuyRealty(AbstractCard acurrentCard)
        {
            if (acurrentCard is AbstractRealtyCard currentCard)
            {
                if (this.Money > currentCard.Cost)
                {
                    this.RealtyCards.Add(currentCard);
                    currentCard.Owner = this;
                    this.Money -= currentCard.Cost;
                    this.MoneyDecreaseEvent?.Invoke(currentCard.Cost, 0);
                }
            }
        }

        internal void BuyRealty(AbstractCard acurrentCard, int cost)
        {
            if (acurrentCard is AbstractRealtyCard currentCard)
            {
                if (this.Money > cost)
                {
                    this.RealtyCards.Add(currentCard);
                    currentCard.Owner = this;
                    this.Money -= cost;
                    this.MoneyDecreaseEvent?.Invoke(cost, 0);
                }
            }
        }

        internal void BuyHouse(AbstractCard acard)
        {
            if (acard is TownCard card)
            {
                if (this.CheckIfOwnCard(card))
                {
                    if (this.Money >= card.HouseCost)
                    {
                        card.AddHouse();
                        this.Money -= card.HouseCost;
                        this.MoneyDecreaseEvent?.Invoke(card.HouseCost, 0);
                    }

                }
                else
                {
                    throw new Exception("Can't build on enemies town!");
                }
            }
        }


        internal void DestroyHouse(AbstractCard acard)
        {
            if (acard is TownCard card)
            {
                if (this.CheckIfOwnCard(card))
                {
                    card.RemoveHouse();
                    this.Money += card.HouseCost/2;
                    this.MoneyIncreaseEvent?.Invoke(card.HouseCost/2, 0);
                }
                else
                {
                    throw new Exception("Can't build on enemies town!");
                }
            }
        }

        internal void AddEventCard(EventCard eventCard)
        {
            this.ActionCards.Add(eventCard);
        }

        internal void DropEventCard(EventCard eventCard)
        {
            this.ActionCards.Remove(eventCard);
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


        internal void PledgeCard(AbstractCard acard)
        {
            if (acard is AbstractRealtyCard card)
            {
                if (this.CheckIfOwnCard(card))
                {
                    card.IsPleged = true;
                    this.Money += card.PledgeCost;
                    this.MoneyIncreaseEvent?.Invoke(card.PledgeCost, 0);
                }
                else
                {
                    throw new Exception("Can't build on enemies town!");
                }
            }
        }


        internal void BuyFromPledgeCard(AbstractCard acard)
        {
            if (acard is AbstractRealtyCard card)
            {
                if (this.CheckIfOwnCard(card))
                {
                    if (this.Money > card.PledgeCost)
                    {
                        card.IsPleged = false;
                        this.Money -= card.PledgeCost;
                        this.MoneyDecreaseEvent?.Invoke(card.PledgeCost, 0);
                    }
                }
                else
                {
                    throw new Exception("Can't build on enemies town!");
                }
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
                if (value > 39)
                {
                    _cardPosition = 0;
                }
                else if (value < 0)
                {
                    _cardPosition = 39;
                }
                else
                {
                    _cardPosition = value;
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
