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
    class TradeAcceptDialogViewModel : BindableBase
    {
        #region Constructors

        public TradeAcceptDialogViewModel(IExchangeManager manager, Action<TradeAcceptResults?> closeAction)
        {
            _manager = manager;
            _closeAction = closeAction;
            this.PlayerLeft = new PlayerViewModel(_manager.PlayerLeft);
            this.PlayerRight = new PlayerViewModel(_manager.PlayerRight);
            this.LeftMoney = _manager.FromPlayerLeftMoney;
            this.RightMoney = _manager.FromPlayerRightMoney;

            this.PlayerLeftCardsToTrade = new ObservableCollection<CardViewModel>(_manager.FromPlayerLeftCards.Select(ac => new CardViewModel(ac,null)));
            this.PlayerRightCardsToTrade = new ObservableCollection<CardViewModel>(_manager.FromPlayerRightCards.Select(ac => new CardViewModel(ac, null)));
        }

        #endregion

        #region Commands

        private DelegateCommand<TradeAcceptResults?> _acceptCommand;
        public DelegateCommand<TradeAcceptResults?> AcceptCommand =>
            _acceptCommand ?? (_acceptCommand = new DelegateCommand<TradeAcceptResults?>(ExecuteAcceptCommand));

        void ExecuteAcceptCommand(TradeAcceptResults? result)
        {
            _closeAction?.Invoke(result);
        }

        #endregion

        #region Properties

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
        private Action<TradeAcceptResults?> _closeAction;

        #endregion
    }
}
