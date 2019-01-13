using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Monopoly.Model.ViewModels
{
    public class PlayerViewModel : BindableBase
    {
        #region Constructors
        public PlayerViewModel(AbstractPlayer player, int order)
        {
            this.Player = player;
            if (this.Player != null)
            {
                this.Player.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "CardPosition")
                    {
                        this.UpdatePos();
                    }
                    this.RaisePropertyChanged(e.PropertyName);
                };

                this.Player.MoneyDecreaseEvent += (i) => Task.Factory.StartNew(() => this.OnMoneyDecrease(i));
                this.Player.MoneyIncreaseEvent += (i) => Task.Factory.StartNew(() => this.OnMoneyIncrease(i));
            }

            this.Order = order;
            this.UpdatePos();
        }

        #endregion

        #region Methods

        private void OnMoneyIncrease(int money)
        {
            //Thread.Sleep(400);
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.IncreaseText = "+" + money.ToString() + "$";
            }), DispatcherPriority.Background);
            //Thread.Sleep(3000);
            //System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            //{
            //    this.IncreaseText = string.Empty;
            //}), DispatcherPriority.Background);
        }

        private void OnMoneyDecrease(int money)
        {
           // Thread.Sleep(400);
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.DecreaseText = "-" + money.ToString() + "$";
            }), DispatcherPriority.Background);
            //Thread.Sleep(3000);
            //System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            //{
            //    this.DecreaseText = string.Empty;
            //}), DispatcherPriority.Background);
        }

        private void UpdatePos()
        {
            if (this.Player.CardPosition == 0)
            {
                this.Pos = new Point(11.5, 11.5);
            }
            else if (this.Player.CardPosition == 1)
            {
                this.Pos = new Point(10, 11.5);
            }
            else if (this.Player.CardPosition > 1 && this.Player.CardPosition < 10)
            {
                this.Pos = new Point(this.Pos.X - 1, this.Pos.Y);
            }
            else if (this.Player.CardPosition == 10)
            {
                this.Pos = new Point(0.5, 11.5);
            }
            else if (this.Player.CardPosition == 11)
            {
                this.Pos = new Point(0.5, 10);
            }
            else if (this.Player.CardPosition > 11 && this.Player.CardPosition < 20)
            {
                this.Pos = new Point(this.Pos.X, this.Pos.Y - 1);
            }
            else if (this.Player.CardPosition == 20)
            {
                this.Pos = new Point(0.5, 0.5);
            }
            else if (this.Player.CardPosition == 21)
            {
                this.Pos = new Point(2, 0.5);
            }
            else if (this.Player.CardPosition > 21 && this.Player.CardPosition < 30)
            {
                this.Pos = new Point(this.Pos.X + 1, this.Pos.Y);
            }
            else if (this.Player.CardPosition == 30)
            {
                this.Pos = new Point(11.5, 0.5);
            }
            else if (this.Player.CardPosition == 31)
            {
                this.Pos = new Point(11.5, 2);
            }
            else if (this.Player.CardPosition > 31 && this.Player.CardPosition < 40)
            {
                this.Pos = new Point(this.Pos.X, this.Pos.Y + 1);
            }
        }

        #endregion

        #region Properties

        private Point _pos;
        public Point Pos
        {
            get { return _pos; }
            set { SetProperty(ref _pos, value); }
        }

        private int _order;
        public int Order
        {
            get { return _order; }
            set { SetProperty(ref _order, value); }
        }

        private string _decreaseText;
        public string DecreaseText
        {
            get { return _decreaseText; }
            set { _decreaseText = value; RaisePropertyChanged("DecreaseText"); }
        }

        private string _increaseText;
        public string IncreaseText
        {
            get { return _increaseText; }
            set { _increaseText = value;RaisePropertyChanged("IncreaseText"); }
        }

        #endregion

        #region Fields

        //own
        public AbstractPlayer Player { get; set; }

        public string Nickname => this.Player.Nickname;
        public string Avatar => this.Player.Avatar;
        public string Chip => this.Player.Chip;
        public int Money => this.Player.Money;
        public ObservableCollection<AbstractCard> RealtyCards => this.Player.RealtyCards;
        public ObservableCollection<AbstractCard> ActionCards => this.Player.ActionCards;
        public bool IsActive => this.Player.IsActive;

        #endregion
    }
}
