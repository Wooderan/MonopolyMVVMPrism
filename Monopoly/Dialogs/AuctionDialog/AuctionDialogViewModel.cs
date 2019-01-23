using Monopoly.Model.Abstract;
using Monopoly.Model.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Dialogs
{
    class AuctionDialogViewModel : BindableBase
    {
        #region Constructors

        public AuctionDialogViewModel(AbstractRealtyCard card, ObservableCollection<AbstractPlayer> players, Action<AbstractPlayer, int> closeAction)
        {
            this.Card = card;
            this.Players = new ObservableCollection<AbstractPlayer>(players);
            this.Close = closeAction;
            this.Title = "Auction";
            _currentPlayer = 0;
            this.Bets = new ObservableCollection<PlayersBet>();
            this.RaisePropertyChanged("Bets");
            this.CurrentSliderValue = 1;
            this.Bets.Insert(0, new PlayersBet(this.Player, (int)this.CurrentSliderValue));
            this.RaisePropertyChanged("LastBet");
            this.CurrentSliderValue = this.LastBet + 1;
            this.NextPlayer();
        }

        #endregion

        #region Commands

        private DelegateCommand _placeBetCommand;
        public DelegateCommand PlaceBetCommand =>
            _placeBetCommand ?? (_placeBetCommand = new DelegateCommand(ExecutePlaceBetCommand));

        void ExecutePlaceBetCommand()
        {
            //this.Bets.Add(new PlayersBet(this.Player, (int)this.CurrentSliderValue));
            this.Bets.Insert(0, new PlayersBet(this.Player, (int)this.CurrentSliderValue));
            this.RaisePropertyChanged("LastBet");
            this.CurrentSliderValue = this.LastBet + 1;
            this.NextPlayer();
        }

        private DelegateCommand _leaveCommand;
        public DelegateCommand LeaveCommand =>
            _leaveCommand ?? (_leaveCommand = new DelegateCommand(ExecuteLeaveCommand));

        void ExecuteLeaveCommand()
        {
            if (this.Players.Count > 1)
            {
                AbstractPlayer currentPlayer = this.Players[CurrentPlayer++];
                AbstractPlayer nextPlayer = this.Players[CurrentPlayer];
                this.Players.Remove(currentPlayer);
                if (this.Players.Count <= 1)
                {
                    this.Close(nextPlayer, this.LastBet);
                }
                else
                {
                    this.CurrentPlayer = this.Players.IndexOf(nextPlayer);
                    this.ComputerAction();
                }
            }
        }

        #endregion

        #region Methods

        void NextPlayer()
        {
            AbstractPlayer currentPlayer = this.Player;
            for (int i = 0; i < this.Players.Count;)
            {
                if (this.Players[i].Money <= this.LastBet)
                {
                    this.Players.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
            if (this.Players.Count <= 1)
            {
                this.Close(currentPlayer, this.LastBet);
            }
            this.CurrentPlayer = this.Players.IndexOf(currentPlayer) + 1;
            this.ComputerAction();
        }

        private void ComputerAction()
        {
            if (this.Player is ComputerPlayer computer)
            {
                int bet = computer.DecideBetOrLeave(this.Card, this.LastBet);
                if (bet != 0)
                {
                    this.Bets.Insert(0, new PlayersBet(this.Player, bet));
                    this.RaisePropertyChanged("LastBet");
                    this.CurrentSliderValue = this.LastBet + 1;
                    this.NextPlayer();
                }
                else
                {
                    this.ExecuteLeaveCommand();
                }
            }
        }

        #endregion

        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private AbstractRealtyCard _card;
        public AbstractRealtyCard Card
        {
            get { return _card; }
            set { SetProperty(ref _card, value); }
        }

        private ObservableCollection<AbstractPlayer> _players;
        public ObservableCollection<AbstractPlayer> Players
        {
            get { return _players; }
            set { SetProperty(ref _players, value); }
        }

        private int _currentPlayer;
        public int CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                if (value < this.Players.Count)
                {
                    SetProperty(ref _currentPlayer, value);
                }
                else
                {
                    SetProperty(ref _currentPlayer, 0);
                }
                RaisePropertyChanged("Player");
            }
        }

        public AbstractPlayer Player { get { return this.Players[_currentPlayer]; } }

        private double _currentSliderValue;
        public double CurrentSliderValue
        {
            get { return _currentSliderValue; }
            set { SetProperty(ref _currentSliderValue, value); }
        }

        public int LastBet
        {
            get
            {
                if (this.Bets.Count > 0)
                {
                    return this.Bets.First().Bet;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region Fields

        private Action<AbstractPlayer, int> Close { get; set; }
        public ObservableCollection<PlayersBet> Bets { get; private set; }

        #endregion
    }
}
