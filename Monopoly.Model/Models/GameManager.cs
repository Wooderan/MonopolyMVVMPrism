using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Linq;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Media;

namespace Monopoly.Model.Models
{
    public class GameManager : BindableBase, IGameManager
    {

        #region Constructors

        public GameManager(ICardLocator cardLocator, IPlayerProvider playerProvider, IDiceProvider diceProvider, IActionProvider actionProvider)
        {
            _cards = cardLocator.GetCardSet();
            _players = playerProvider.GetPlayers();
            _diceProvider = diceProvider;
            _actionProvider = actionProvider;
            this.Cards = new ReadOnlyObservableCollection<AbstractCard>(_cards);
            this.CurrentPlayer = 0;
            this.Players[CurrentPlayer].IsActive = true;
            this.Draws = 1;

            this.PrepareForTesting();
        }

        #endregion

        #region Events
        public event Action<AbstractCard> ShowBuyOrAuctionDialog;
        public event Action<AbstractCard, ObservableCollection<AbstractPlayer>> ShowAuctionDialog;
        #endregion

        #region Methods

        private void PrepareForTesting()
        {
            this.Players[0].BuyRealty(this.Cards[2]);
        }

        public void NextPlayer()
        {
            this.Players[CurrentPlayer].IsActive = false;
            int activePlayers = this.Players.Where(ap => ap.HaveMoney).ToList().Count;
            if (activePlayers > 1)
            {
                do
                {
                    this.CurrentPlayer++;
                    if (this.CurrentPlayer >= this.Players.Count)
                    {
                        this.CurrentPlayer = 0;
                    }
                } while (!this.Players[CurrentPlayer].HaveMoney);
                this.Players[CurrentPlayer].IsActive = true;
                this.Draws = 1;
                this.RaisePropertyChanged("HasMonopoly");
                this.RaisePropertyChanged("HasBuildings");
                this.RaisePropertyChanged("HasTowns");
                this.RaisePropertyChanged("HasMortgageTowns");
            }
            else
            {
                throw new Exception("Сделай конец игры! Остался только один игрок!");
            }
        }

        public void MakeDiceRoll()
        {
            int result;
            if (!_diceProvider.RollDice(out result))
            {
                this.Draws--;
            }
            this.MakeStep(result);
        }

        private void AfterStep()
        {
            AbstractCard currentCard = this.Cards[this.Players[CurrentPlayer].CardPosition];
            if (currentCard is AbstractRealtyCard realtyCard)
            {
                if (realtyCard.Owner == null)
                {
                    if (realtyCard.CardGroup.GroupColor == Colors.Gray)
                    {
                        this.ShowAuctionDialog?.Invoke(realtyCard, this.Players);
                    }
                    else
                    {
                        this.ShowBuyOrAuctionDialog?.Invoke(realtyCard);
                    }
                }
                else
                {
                    this.PayTax();
                }
            }
            else if (currentCard is EventCard eventCard)
            {
                eventCard.EventAction?.Invoke(this);
            }
        }

        private void MakeStep()
        {
            this.Players[CurrentPlayer].CardPosition++;
        }

        private void MakeStep(int count)
        {
            Task.Factory.StartNew(() => this.StepByStep(count));
        }

        private void StepByStep(int count)
        {
            for (int i = count; i > 0; i--)
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.MakeStep();
                }), DispatcherPriority.Background);
                Thread.Sleep(300);
            }
            this.AfterStep();
        }

        public void Buy()
        {
            AbstractCard currentCard = this.Cards[this.Players[CurrentPlayer].CardPosition];
            if (currentCard is TownCard || currentCard is StationCard)
            {
                this.Players[CurrentPlayer].BuyRealty(currentCard);
                this.RaisePropertyChanged("HasMonopoly");
                this.RaisePropertyChanged("HasBuildings");
                this.RaisePropertyChanged("HasTowns");
            }
            else
            {
                throw new Exception("Можно покупать только города! Єто не должно было случится! Ошибка в коде, разработчик олень!");
            }
        }

        public void Buy(AbstractPlayer winner, int cost)
        {
            AbstractCard currentCard = this.Cards[this.Players[CurrentPlayer].CardPosition];
            if (currentCard is TownCard || currentCard is StationCard)
            {
                winner.BuyRealty(currentCard, cost);
                this.RaisePropertyChanged("HasMonopoly");
                this.RaisePropertyChanged("HasBuildings");
                this.RaisePropertyChanged("HasTowns");
            }
            else
            {
                throw new Exception("Можно покупать только города! Єто не должно было случится! Ошибка в коде, разработчик олень!");
            }
        }

        private void PayTax()
        {
            AbstractCard currentCard = this.Cards[this.Players[CurrentPlayer].CardPosition];
            if (currentCard is AbstractRealtyCard realtyCard)
            {
                this.Players[CurrentPlayer].PayTax(realtyCard.CurrentTax);
                realtyCard.Owner.GetTax(realtyCard.CurrentTax);
            }
            else
            {
                throw new Exception("Платить налоги можно только в городах! Єто не должно было случится! Ошибка в коде, разработчик олень!");
            }
        }

        public void BuildHouse(AbstractCard acard)
        {
            if (acard is TownCard card)
            {
                if (card.Houses < 5)
                {
                    this.Players[CurrentPlayer].BuyHouse(card);
                    this.RaisePropertyChanged("HasBuildings");
                }
            }
        }

        public void DestroyHouse(AbstractCard acard)
        {
            if (acard is TownCard card)
            {
                if (card.Houses > 0)
                {
                    this.Players[CurrentPlayer].DestroyHouse(card);
                    this.RaisePropertyChanged("HasBuildings");
                }
            }
        }

        public void PledgeCard(AbstractCard acard)
        {
            if (acard is AbstractRealtyCard card)
            {
                if (!card.IsPleged)
                {
                    this.Players[CurrentPlayer].PledgeCard(card);

                    this.RaisePropertyChanged("HasTowns");
                    this.RaisePropertyChanged("HasMortgageCards");
                    this.RaisePropertyChanged("HasMonopoly");
                }
            }
        }

        public void BuyFromPledgeCard(AbstractCard acard)
        {
            if (acard is AbstractRealtyCard card)
            {
                if (card.IsPleged)
                {
                    this.Players[CurrentPlayer].BuyFromPledgeCard(card);

                    this.RaisePropertyChanged("HasTowns");
                    this.RaisePropertyChanged("HasMortgageCards");
                    this.RaisePropertyChanged("HasMonopoly");
                }
            }
        }

        public void ShowAuction()
        {
            AbstractCard currentCard = this.Cards[this.Players[CurrentPlayer].CardPosition];
            this.ShowAuctionDialog?.Invoke(currentCard, this.Players);
        }

        public void GiveMoney(int money)
        {
            this.Players[CurrentPlayer].PickUpMoney(money, 0);
        }

        public void TakeMoney(int money)
        {
            this.Players[CurrentPlayer].ThrowMoney(money, 0);
        }

        public void ChestAction()
        {
            GameAction action = _actionProvider.GetRandomChestAction();
            action.Action?.Invoke(this);
        }

        public void LuckyAction()
        {
            GameAction action = _actionProvider.GetRandomLuckyAction();
            action.Action?.Invoke(this);
        }

        #endregion

        #region Fields

        private ObservableCollection<AbstractCard> _cards;
        private ObservableCollection<AbstractPlayer> _players;
        private int _currentPlayer;
        private int _draws;
        private IDiceProvider _diceProvider;
        private IActionProvider _actionProvider;

        public ReadOnlyObservableCollection<AbstractCard> Cards { get; protected set; }
        public ObservableCollection<AbstractPlayer> Players => _players;
        public int CurrentPlayer { get => _currentPlayer; private set => _currentPlayer = value; }

        public int Draws { get => _draws; private set { _draws = value; this.RaisePropertyChanged(); this.RaisePropertyChanged("HaveDraws"); this.RaisePropertyChanged("HaveNotDraws"); } }
        public bool HaveDraws { get => this.Draws > 0;}
        public bool HaveNotDraws { get => !this.HaveDraws;}
        public bool HasMonopoly { get => this.Players[CurrentPlayer].RealtyCards.Where(ac => (ac is TownCard) && (ac as TownCard).CardGroup.IsMonopoly).Any(); }
        public bool HasBuildings { get => this.Players[CurrentPlayer].RealtyCards.Where(ac => (ac is TownCard) && (ac as TownCard).Houses > 0).Any(); }
        public bool HasTowns { get => this.Players[CurrentPlayer].RealtyCards.Count > 0; }
        public bool HasMortgageCards { get => this.Players[CurrentPlayer].RealtyCards.Where(ac => (ac is AbstractRealtyCard) && (ac as AbstractRealtyCard).IsPleged).Any(); }



        #endregion
    }
}
