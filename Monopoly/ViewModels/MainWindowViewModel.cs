using System;
using System.Collections.ObjectModel;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Monopoly.Dialogs;
using Monopoly.Model.Abstract;
using Monopoly.Model.Events;
using Monopoly.Model.Interfaces;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace Monopoly.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Constructor

        public MainWindowViewModel(IGameManager gameManager, IEventAggregator eventAggregator)
        {
            this.GameManager = gameManager;
            this.GameManager.ShowBuyOrAuctionDialog += GameManager_ShowBuyOrAuctionDialog;
            this.GameManager.ShowAuctionDialog += GameManager_ShowAuctionDialog;

            eventAggregator.GetEvent<ShowPlayerDetailEvent>().Subscribe(this.OpenLeftFlyout);
        }

        #endregion

        #region Methods

        private void OpenLeftFlyout(AbstractPlayer player)
        {
            this.IsLeftFlyoutOpened = true;
        }

        private void GameManager_ShowBuyOrAuctionDialog(AbstractCard card)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var dialog = new BuyOrAuctionDialog();
                var viewModel = new BuyOrAuctionDialogViewModel(card, dr =>
                {
                    _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    if (dr == BuyOrAuctionDialogResults.BUY)
                    {
                        this.GameManager.Buy();
                    }
                    else if (dr == BuyOrAuctionDialogResults.AUCTION)
                    {
                        this.GameManager.ShowAuction();
                    }
                });
                dialog.DataContext = viewModel;

                _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });

        }


        private void GameManager_ShowAuctionDialog(AbstractCard card, ObservableCollection<AbstractPlayer> players)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var dialog = new AuctionDialog();
                var viewModel = new AuctionDialogViewModel(card, players, (winner, cost) =>
                {
                    _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    this.GameManager.Buy(winner, cost);
                });
                dialog.DataContext = viewModel;

                _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });
        }

        #endregion

        #region Properties

        private bool _isLeftFlyoutOpened;
        public bool IsLeftFlyoutOpened
        {
            get { return _isLeftFlyoutOpened; }
            set { SetProperty(ref _isLeftFlyoutOpened, value); }
        }

        #endregion

        #region Fields

        public IGameManager GameManager { get; private set; }

        private IDialogCoordinator _dialogCoordinator;
        public IDialogCoordinator DialogCoordinator { get => _dialogCoordinator; set => _dialogCoordinator = value; }

        #endregion
    }
}
