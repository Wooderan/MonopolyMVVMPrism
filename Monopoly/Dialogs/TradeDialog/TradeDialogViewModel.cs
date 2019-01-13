using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Monopoly.Model.ViewModels;
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
    class TradeDialogViewModel : BindableBase
    {
        #region Constructors

        public TradeDialogViewModel(IExchangeManager manager, Action<TradeDialogResults?> closeAction)
        {
            _manager = manager;
            _closeAction = closeAction;
            this.PlayerLeft = new PlayerViewModel(_manager.PlayerLeft);
            this.PlayerRight = new PlayerViewModel(_manager.PlayerRight);
            this.LeftMoney = _manager.FromPlayerLeftMoney;
            this.RightMoney = _manager.FromPlayerRightMoney;

            this.PlayerLeftCardsHave = new ObservableCollection<CardViewModel>(this.PlayerLeft.RealtyCards.Select(ac => new CardViewModel(ac, null)));
            this.PlayerRightCardsHave = new ObservableCollection<CardViewModel>(this.PlayerRight.RealtyCards.Select(ac => new CardViewModel(ac, null)));
            this.PlayerLeftCardsToTrade = new ObservableCollection<CardViewModel>();
            this.PlayerRightCardsToTrade = new ObservableCollection<CardViewModel>();
        }

        #endregion

        #region Commands

        private DelegateCommand<TradeDialogResults?> _tradeCommand;
        public DelegateCommand<TradeDialogResults?> TradeCommand =>
            _tradeCommand ?? (_tradeCommand = new DelegateCommand<TradeDialogResults?>(ExecuteTradeCommand));

        void ExecuteTradeCommand(TradeDialogResults? result)
        {
            _manager.FromPlayerLeftMoney = this.LeftMoney;
            _manager.FromPlayerRightMoney = this.RightMoney;
            foreach (CardViewModel cvm in this.PlayerLeftCardsToTrade)
            {
                _manager.FromPlayerLeftCards.Add(cvm.Card);
            }
            foreach (CardViewModel cvm in this.PlayerRightCardsToTrade)
            {
                _manager.FromPlayerRightCards.Add(cvm.Card);
            }
            _closeAction?.Invoke(result);
        }

        private DelegateCommand<CardViewModel> _addCardToLeftTrade;
        public DelegateCommand<CardViewModel> AddCardToLeftTrade =>
            _addCardToLeftTrade ?? (_addCardToLeftTrade = new DelegateCommand<CardViewModel>(ExecuteAddCardToLeftTrade));

        void ExecuteAddCardToLeftTrade(CardViewModel card)
        {
            this.PlayerLeftCardsHave.Remove(card);
            this.PlayerLeftCardsToTrade.Add(card);
        }

        private DelegateCommand<CardViewModel> _addCardToRightTrade;
        public DelegateCommand<CardViewModel> AddCardToRightTrade =>
            _addCardToRightTrade ?? (_addCardToRightTrade = new DelegateCommand<CardViewModel>(ExecuteAddCardToRightTrade));

        void ExecuteAddCardToRightTrade(CardViewModel card)
        {
            this.PlayerRightCardsHave.Remove(card);
            this.PlayerRightCardsToTrade.Add(card);
        }

        #endregion

        #region Properties

        public ObservableCollection<CardViewModel> PlayerLeftCardsHave { get; private set; }
        public ObservableCollection<CardViewModel> PlayerRightCardsHave { get; private set; }

        public ObservableCollection<CardViewModel> PlayerLeftCardsToTrade { get; private set; }
        public ObservableCollection<CardViewModel> PlayerRightCardsToTrade { get; private set; }

        private PlayerViewModel _playerLeft;
        public PlayerViewModel PlayerLeft
        {
            get { return _playerLeft; }
            set { SetProperty(ref _playerLeft, value); }
        }

        private PlayerViewModel _playerRight;
        public PlayerViewModel PlayerRight
        {
            get { return _playerRight; }
            set { SetProperty(ref _playerRight, value); }
        }

        private int _leftMoney;
        public int LeftMoney
        {
            get { return _leftMoney; }
            set { SetProperty(ref _leftMoney, value); }
        }

        private int _rightMoney;
        public int RightMoney
        {
            get { return _rightMoney; }
            set { SetProperty(ref _rightMoney, value); }
        }

        #endregion

        #region Fields

        private IExchangeManager _manager;
        private Action<TradeDialogResults?> _closeAction;

        #endregion
    }
}
