using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Commands;
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
        public PlayerViewModel(AbstractPlayer player, int order = 1)
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
                    else if (e.PropertyName == "CantAct")
                    {
                        this.RaisePropertyChanged("IsJailed");
                    }
                    this.RaisePropertyChanged(e.PropertyName);
                };

                this.Player.MoneyDecreaseEvent += (i, d) => Task.Factory.StartNew(() => this.OnMoneyDecrease(i, d));
                this.Player.MoneyIncreaseEvent += (i, d) => Task.Factory.StartNew(() => this.OnMoneyIncrease(i, d));
            }

            this.Order = order;
            this.UpdatePos();
        }

        #endregion

        #region Methods

        private async void OnMoneyIncrease(int money, int delay)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            //Thread.Sleep(TimeSpan.FromSeconds(delay));
            await System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.IncreaseText = "+" + money.ToString() + "$";
            }), DispatcherPriority.Background);
            //Thread.Sleep(3000);
            //System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            //{
            //    this.IncreaseText = string.Empty;
            //}), DispatcherPriority.Background);
        }

        private async void OnMoneyDecrease(int money, int delay)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
            //Thread.Sleep(TimeSpan.FromSeconds(delay));
            await System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
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
            switch (this.Player.CardPosition)
            {
                case 0: this.Pos = new Point(11.5, 11.5); break;
                case 1: this.Pos = new Point(10, 11.5); break;
                case 2: this.Pos = new Point(9, 11.5); break;
                case 3: this.Pos = new Point(8, 11.5); break;
                case 4: this.Pos = new Point(7, 11.5); break;
                case 5: this.Pos = new Point(6, 11.5); break;
                case 6: this.Pos = new Point(5, 11.5); break;
                case 7: this.Pos = new Point(4, 11.5); break;
                case 8: this.Pos = new Point(3, 11.5); break;
                case 9: this.Pos = new Point(2, 11.5); break;
                case 10: this.Pos = new Point(0.5, 11.5); break;
                case 11: this.Pos = new Point(0.5, 10); break;
                case 12: this.Pos = new Point(0.5, 9); break;
                case 13: this.Pos = new Point(0.5, 8); break;
                case 14: this.Pos = new Point(0.5, 7); break;
                case 15: this.Pos = new Point(0.5, 6); break;
                case 16: this.Pos = new Point(0.5, 5); break;
                case 17: this.Pos = new Point(0.5, 4); break;
                case 18: this.Pos = new Point(0.5, 3); break;
                case 19: this.Pos = new Point(0.5, 2); break;
                case 20: this.Pos = new Point(0.5, 0.5); break;
                case 21: this.Pos = new Point(2, 0.5); break;
                case 22: this.Pos = new Point(3, 0.5); break;
                case 23: this.Pos = new Point(4, 0.5); break;
                case 24: this.Pos = new Point(5, 0.5); break;
                case 25: this.Pos = new Point(6, 0.5); break;
                case 26: this.Pos = new Point(7, 0.5); break;
                case 27: this.Pos = new Point(8, 0.5); break;
                case 28: this.Pos = new Point(9, 0.5); break;
                case 29: this.Pos = new Point(10, 0.5); break;
                case 30: this.Pos = new Point(11.5, 0.5); break;
                case 31: this.Pos = new Point(11.5, 2); break;
                case 32: this.Pos = new Point(11.5, 3); break;
                case 33: this.Pos = new Point(11.5, 4); break;
                case 34: this.Pos = new Point(11.5, 5); break;
                case 35: this.Pos = new Point(11.5, 6); break;
                case 36: this.Pos = new Point(11.5, 7); break;
                case 37: this.Pos = new Point(11.5, 8); break;
                case 38: this.Pos = new Point(11.5, 9); break;
                case 39: this.Pos = new Point(11.5, 10); break;
                default:
                    break;
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

        public bool IsJailed { get => this.Player.CantAct > 0; }

        #endregion

        #region Commands

        private DelegateCommand<object> _clickCommand;
        public DelegateCommand<object> ClickCommand =>
            _clickCommand ?? (_clickCommand = new DelegateCommand<object>(ExecuteClickCommand));

        void ExecuteClickCommand(object obj)
        {
            this.clickAction?.Invoke(obj);
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

        public Action<object> clickAction;

        #endregion
    }
}
