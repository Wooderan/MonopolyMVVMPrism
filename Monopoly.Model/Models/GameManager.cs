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
using Prism.Events;
using Monopoly.Model.Events;
using System.ComponentModel;

namespace Monopoly.Model.Models
{
    public class GameManager : BindableBase, IGameManager
    {

        #region Constructors

        public GameManager(ICardLocator cardLocator, IPlayerProvider playerProvider, IDiceProvider diceProvider, IActionProvider actionProvider, IEventAggregator eventAggregator)
        {
            _cardLocator = cardLocator;
            _playerProvider = playerProvider;
            _diceProvider = diceProvider;
            _actionProvider = actionProvider;
            _eventAggregator = eventAggregator;
            _cards = _cardLocator.GetCardSet();
            this.Cards = new ReadOnlyObservableCollection<AbstractCard>(_cards);

            this.ReloadGame();
        }

        #endregion

        #region Events
        public event Action<AbstractCard> ShowBuyOrAuctionDialog;
        public event Action<AbstractRealtyCard, ObservableCollection<AbstractPlayer>> ShowAuctionDialog;
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
                this.RaisePropertyChanged("CanAct");

                AbstractPlayer player = this.Players[CurrentPlayer];
                if (this.Cards[player.CardPosition] is EventCard eventCard && eventCard.Name == "Jail" && player.CantAct > 0)
                {
                    _eventAggregator.GetEvent<JailDialogEvent>().Publish(player);
                }
                else if (player is ComputerPlayer computer)
                {
                    this.MakeDiceRoll();
                }
            }
            else
            {
                throw new Exception("Сделай конец игры! Остался только один игрок!");
            }
        }

        public void MakeDiceRoll()
        {
            AbstractPlayer player = this.Players[CurrentPlayer];
            _diceProvider.RollDice();
            Action stepAction;

            if (_diceProvider.LeftCube == _diceProvider.RightCube)
            {
                stepAction = () => this.MakeStep(_diceProvider.Result);
                _eventAggregator.GetEvent<ShowDiceRollingEvent>().Publish(stepAction);
            }
            else
            {
                stepAction = () => { this.Draws--; this.MakeStep(_diceProvider.Result); };
                _eventAggregator.GetEvent<ShowDiceRollingEvent>().Publish(stepAction);
            }

        }

        private void AfterStep()
        {
            AbstractPlayer player = this.Players[CurrentPlayer];
            AbstractCard currentCard = this.Cards[this.Players[CurrentPlayer].CardPosition];
            if (currentCard is AbstractRealtyCard realtyCard)
            {
                if (realtyCard.Owner == null)
                {
                    if (realtyCard.CardGroup.GroupColor == Colors.Gray || player.Money <= realtyCard.Cost)
                    {
                        this.ShowAuction();
                    }
                    else
                    {
                        if (player is ComputerPlayer computer)
                        {
                            if (computer.DecideBuyOrAuction(realtyCard))
                            {
                                this.Buy();
                                this.AfterAction();
                            }
                            else
                            {
                                this.ShowAuctionDialog?.Invoke(realtyCard, this.Players);
                            }
                        }
                        else
                        {
                            this.ShowBuyOrAuctionDialog?.Invoke(realtyCard);
                        }
                    }
                }
                else
                {
                    if (realtyCard.Owner != player)
                    {
                        this.PayTax();
                    }
                    this.AfterAction();
                }
            }
            else if (currentCard is EventCard eventCard)
            {
                if (eventCard.EventAction is null)
                {
                    this.AfterAction();
                }
                else
                {
                    eventCard.EventAction.Invoke(this);
                }
            }
        }

        public void AfterAction()
        {
            AbstractPlayer player = this.Players[CurrentPlayer];
            if (player is ComputerPlayer computer)
            {
                if (this.Draws > 0)
                {
                    this.MakeDiceRoll();
                }
                else
                {
                    computer.DecideAfterStep(this);
                }
            }
        }

        public void ActivateEventCard(EventCard eventCard)
        {
            eventCard.EventAction?.Invoke(this);
            this.Players[CurrentPlayer].DropEventCard(eventCard);
        }
        public void GiveEventCard(EventCard eventCard)
        {
            this.Players[CurrentPlayer].AddEventCard(eventCard);
        }

        //true - forward
        //false - back
        private void MakeStep(bool direction = true)
        {
            if (direction)
            {
                this.Players[CurrentPlayer].CardPosition++;
            }
            else
            {
                this.Players[CurrentPlayer].CardPosition--;
            }
        }

        private void MakeStep(int count, bool direction = true)
        {
            if (direction)
            {
                Task.Factory.StartNew(() => this.StepByStep(count));
            }
            else
            {
                Task.Factory.StartNew(() => this.StepByStep(count, false));
            }
        }

        private void StepByStep(int count, bool direction = true)
        {
            this.Animating = true;
            for (int i = count; i > 0; i--)
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (direction)
                    {
                        this.MakeStep();
                    }
                    else
                    {
                        this.MakeStep(false);
                    }
                }), DispatcherPriority.Background);
                Thread.Sleep(300);
            }
            this.Animating = false;
            this.AfterStep();
        }

        public void Buy()
        {
            AbstractCard currentCard = this.Cards[this.Players[CurrentPlayer].CardPosition];
            if (currentCard is TownCard || currentCard is StationCard)
            {
                AbstractPlayer currentPlayer = this.Players[CurrentPlayer];
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    currentPlayer.BuyRealty(currentCard);
                }), DispatcherPriority.Background);
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
            AbstractRealtyCard currentCard = this.Cards[this.Players[CurrentPlayer].CardPosition] as AbstractRealtyCard;
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
            this.GameAction(action);
        }

        public void LuckyAction()
        {
            GameAction action = _actionProvider.GetRandomLuckyAction();
            this.GameAction(action);
        }

        public void GameAction(GameAction action)
        {
            _eventAggregator.GetEvent<GameActionEvent>().Publish(action);
        }

        public void GoToJail()
        {
            int jailPos = 0;
            foreach (AbstractCard card in this.Cards)
            {
                if (card is EventCard eventCard)
                {
                    if (eventCard.Name == "Jail")
                    {
                        jailPos = this.Cards.IndexOf(card);
                    }
                }
            }

            AbstractPlayer player = this.Players[CurrentPlayer];
            player.CantAct = 3;
            this.Draws = 0;
            this.RaisePropertyChanged("CanAct");
            if (player.CardPosition != jailPos)
            {
                if (player.CardPosition > jailPos)
                {
                    this.MakeStep(player.CardPosition - jailPos, false);
                }
                else
                {
                    this.MakeStep(jailPos - player.CardPosition);
                }
            }
        }

        public void FreeFromJail()
        {
            AbstractPlayer player = this.Players[CurrentPlayer];
            player.CantAct = 0;
            this.Draws = 1;
            this.RaisePropertyChanged("CanAct");
        }

        public void ReloadGame()
        {
            
            if (_players is null)
            {
                _players = new ObservableCollection<AbstractPlayer>();
            }
            else
            {
                _players?.Clear();
            }
            foreach (AbstractPlayer player in _playerProvider.GetPlayers())
            {
                _players.Add(player);
            }

            this.CurrentPlayer = 0;
            this.Players[CurrentPlayer].IsActive = true;
            this.Draws = 1;

            //this.RaisePropertyChanged("Cards");
            this.RaisePropertyChanged("Players");
            this.PrepareForTesting();
        }


        #endregion

        #region Fields

        private ObservableCollection<AbstractCard> _cards;
        private ObservableCollection<AbstractPlayer> _players;
        private int _currentPlayer;
        private int _draws;
        private IDiceProvider _diceProvider;
        private IActionProvider _actionProvider;
        private IEventAggregator _eventAggregator;
        private IPlayerProvider _playerProvider;
        private ICardLocator _cardLocator;
        private bool _animating;

        public ReadOnlyObservableCollection<AbstractCard> Cards { get; protected set; }
        public ObservableCollection<AbstractPlayer> Players => _players;
        public int CurrentPlayer { get => _currentPlayer; private set => _currentPlayer = value; }

        public int Draws { get => _draws; private set { _draws = value; this.RaisePropertyChanged(); this.RaisePropertyChanged("HaveDraws"); this.RaisePropertyChanged("HaveNotDraws"); } }
        public bool HaveDraws { get => this.Draws > 0; }
        public bool HaveNotDraws { get => !this.HaveDraws; }
        public bool HasMonopoly { get => this.Players[CurrentPlayer].RealtyCards.Where(ac => (ac is TownCard) && (ac as TownCard).CardGroup.IsMonopoly).Any(); }
        public bool HasBuildings { get => this.Players[CurrentPlayer].RealtyCards.Where(ac => (ac is TownCard) && (ac as TownCard).Houses > 0).Any(); }
        public bool HasTowns { get => this.Players[CurrentPlayer].RealtyCards.Count > 0; }
        public bool HasMortgageCards { get => this.Players[CurrentPlayer].RealtyCards.Where(ac => (ac is AbstractRealtyCard) && (ac as AbstractRealtyCard).IsPleged).Any(); }
        public bool CanAct { get => this.Players[CurrentPlayer].CantAct == 0; }
        public bool Animating{get { return _animating; } set { SetProperty(ref _animating, value); }}

        #endregion
    }
}
