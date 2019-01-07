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

namespace Monopoly.Model.Models
{
    public class GameManager : BindableBase, IGameManager
    {

        #region Constructors

        public GameManager(ICardLocator cardLocator, IPlayerProvider playerProvider)
        {
            _cards = cardLocator.GetCardSet();
            _players = playerProvider.GetPlayers();
            this.Cards = new ReadOnlyObservableCollection<AbstractCard>(_cards);
            this.CurrentPlayer = 0;
            this.Players[CurrentPlayer].IsActive = true;
            this.Draws = 1;
        }

        #endregion

        #region Events
        public event Action ShowBuyOrAuctionDialog;
        #endregion

        #region Methods

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
            }
            else
            {
                throw new Exception("Сделай конец игры! Остался только один игрок!");
            }
        }

        public void MakeDraw()
        {
            Random rand = new Random();
            this.MakeStep(rand.Next(2,12));
            this.Draws--;
        }

        private void AfterStep()
        {
            AbstractCard currentCard = this.Cards[this.Players[CurrentPlayer].CardPosition];
            if (currentCard is TownCard)
            {
                if (currentCard.Owner == null)
                {
                    this.ShowBuyOrAuctionDialog?.Invoke();
                }
                else
                {
                    this.PayTax();
                }
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
            if (currentCard is TownCard)
            {
                this.Players[CurrentPlayer].BuyTown(currentCard);
            }
            else
            {
                throw new Exception("Можно покупать только города! Єто не должно было случится! Ошибка в коде, разработчик олень!");
            }
        }

        private void PayTax()
        {
            AbstractCard currentCard = this.Cards[this.Players[CurrentPlayer].CardPosition];
            if (currentCard is TownCard)
            {
                this.Players[CurrentPlayer].PayTax(currentCard.CurrentTax);
                currentCard.Owner.GetTax(currentCard.CurrentTax);
            }
            else
            {
                throw new Exception("Платить налоги можно только в городах! Єто не должно было случится! Ошибка в коде, разработчик олень!");
            }
        }

        #endregion

        #region Fields

        private ObservableCollection<AbstractCard> _cards;
        private ObservableCollection<AbstractPlayer> _players;
        private int _currentPlayer;
        private int _draws;

        public ReadOnlyObservableCollection<AbstractCard> Cards { get; protected set; }
        public ObservableCollection<AbstractPlayer> Players => _players;
        public int CurrentPlayer { get => _currentPlayer; private set => _currentPlayer = value; }

        public int Draws { get => _draws; private set { _draws = value; this.RaisePropertyChanged(); this.RaisePropertyChanged("HaveDraws"); this.RaisePropertyChanged("HaveNotDraws"); } }
        public bool HaveDraws { get => this.Draws > 0;}
        public bool HaveNotDraws { get => !this.HaveDraws;}

        #endregion
    }
}
