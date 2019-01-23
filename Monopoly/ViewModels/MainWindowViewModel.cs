using System;
using System.Linq;
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
using Monopoly.Model.Models;
using Monopoly.Model.Model;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Monopoly.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Constructor

        public MainWindowViewModel(IGameManager gameManager, IEventAggregator eventAggregator, IExchangeManager exchangeManager, IDiceProvider diceProvider)
        {
            this.GameManager = gameManager;
            this.ExchangeManager = exchangeManager;
            this.GameManager.ShowBuyOrAuctionDialog += GameManager_ShowBuyOrAuctionDialog;
            this.GameManager.ShowAuctionDialog += GameManager_ShowAuctionDialog;
            _diceProvider = diceProvider;

            eventAggregator.GetEvent<ShowPlayerDetailEvent>().Subscribe(this.OpenLeftFlyout);
            eventAggregator.GetEvent<TradeEvent>().Subscribe(this.ChooseTradeOpponentDialog);
            eventAggregator.GetEvent<GameActionEvent>().Subscribe(this.GameActionDialog);
            eventAggregator.GetEvent<ShowDiceRollingEvent>().Subscribe(this.DiceRollingDialog);
            eventAggregator.GetEvent<JailDialogEvent>().Subscribe(this.JailDialog);
            eventAggregator.GetEvent<ShowAvatarsEvent>().Subscribe(this.AvatarDialog);
            eventAggregator.GetEvent<ShowChipsEvent>().Subscribe(this.ChipDialog);
        }

        #endregion

        #region Methods

        private void OpenLeftFlyout(AbstractPlayer player)
        {
            this.IsLeftFlyoutOpened = true;
        }

        private void ChooseTradeOpponentDialog()
        {
            AbstractPlayer player = this.GameManager.Players[this.GameManager.CurrentPlayer];
            ObservableCollection<AbstractPlayer> otherPlayers = new ObservableCollection<AbstractPlayer>();
            foreach (AbstractPlayer ap in this.GameManager.Players.Except(new ObservableCollection<AbstractPlayer>() { player }))
            {
                otherPlayers.Add(ap);
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                var dialog = new ChooseTradeOpponentDialog();
                var viewModel = new ChooseTradeOpponentDialogViewModel(otherPlayers, opponent =>
                {
                    _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    if (opponent != null)
                    {
                        this.TradeDialog(player, opponent);
                    }
                });
                dialog.DataContext = viewModel;

                _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });
        }

        private void TradeDialog(AbstractPlayer playerLeft, AbstractPlayer playerRight)
        {
            this.ExchangeManager.SetPlayers(playerLeft, playerRight);
            Application.Current.Dispatcher.Invoke(() =>
            {
                
                var dialog = new TradeDialog();
                var viewModel = new TradeDialogViewModel(this.ExchangeManager, result =>
                {
                    _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    if (result == TradeDialogResults.OFFER)
                    {
                        this.TradeAcceptDialog();
                    }
                });
                dialog.DataContext = viewModel;

                _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });
        }

        private void TradeAcceptDialog()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                var dialog = new TradeAcceptDialog();
                var viewModel = new TradeAcceptDialogViewModel(this.ExchangeManager, result =>
                {
                    _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    if (result == TradeAcceptResults.ACCEPT)
                    {
                        this.ExchangeManager.Exchange();
                    }
                });
                dialog.DataContext = viewModel;

                _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });
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


        private void GameManager_ShowAuctionDialog(AbstractRealtyCard card, ObservableCollection<AbstractPlayer> players)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var dialog = new AuctionDialog();
                var viewModel = new AuctionDialogViewModel(card, players, (winner, cost) =>
                {
                    _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    this.GameManager.Buy(winner, cost);
                    this.GameManager.AfterAction();
                });
                dialog.DataContext = viewModel;

                _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });
        }


        private void GameActionDialog(GameAction gAction)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var dialog = new GameActionDialog();
                var viewModel = new GameActionDialogViewModel(gAction.Message, () =>
                {
                    _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    gAction.Action?.Invoke(this.GameManager);
                    this.GameManager.AfterAction();
                });
                dialog.DataContext = viewModel;

                _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });
        }


        private void DiceRollingDialog(Action afterAction)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var dialog = new ShowDiceRollingDialog();
                var viewModel = new ShowDiceRollingDialogViewModel(_diceProvider.LeftCube, _diceProvider.RightCube, () =>
                {
                    _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    afterAction?.Invoke();
                });
                dialog.DataContext = viewModel;

                _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });
        }


        private void JailDialog(AbstractPlayer player)
        {
            bool haveEscapeActionCard = player.ActionCards.Count > 0 && player.ActionCards.Where(ac => ac.Name == "Escape Plan").Any();
            Application.Current.Dispatcher.Invoke(() =>
            {
                var dialog = new JailDialog();
                var viewModel = new JailDialogViewModel(haveEscapeActionCard, (result) =>
                {
                    _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    if (result == JailDialogResults.ACTIONCARD)
                    {
                        this.GameManager.ActivateEventCard(player.ActionCards.Where(ac => ac.Name == "Escape Plan").First() as EventCard);
                    }
                    else if (result == JailDialogResults.PAY)
                    {
                        this.GameManager.TakeMoney(100);
                        this.GameManager.FreeFromJail();
                    }
                    else
                    {
                        _diceProvider.RollDice();
                        Action afterAction = () => { };
                        if (_diceProvider.LeftCube == _diceProvider.RightCube)
                        {
                            afterAction = () => { this.GameManager.FreeFromJail(); };
                        }
                        this.DiceRollingDialog(afterAction);
                    }
                    this.GameManager.AfterAction();
                });
                dialog.DataContext = viewModel;

                _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });
        }


        private void AvatarDialog(PlayerParameters parameters)
        {
            List<string> images = new List<string>();
            int counter = 1;
            Uri imageUri = new Uri("pack://application:,,,/Monopoly.Model;component/Images/Avatars/avatar-" + counter++.ToString() +".png", UriKind.Absolute);
            try
            {
                while (new BitmapImage(imageUri) != null)
                {
                    images.Add(imageUri.ToString());
                    imageUri = new Uri("pack://application:,,,/Monopoly.Model;component/Images/Avatars/avatar-" + counter++.ToString() +".png", UriKind.Absolute);
                }
            }
            catch (Exception){}

            Application.Current.Dispatcher.Invoke(() =>
            {
                var dialog = new ImagesDialog();
                var viewModel = new ImagesDialogViewModel(images, (img) =>
                {
                    _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    parameters.Avatar = img;
                });
                dialog.DataContext = viewModel;

                _dialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });
        }

        private void ChipDialog(PlayerParameters parameters)
        {
            List<string> images = new List<string>();
            int counter = 1;
            Uri imageUri = new Uri("pack://application:,,,/Monopoly.Model;component/Images/Chips/chip-" + counter++.ToString() + ".png", UriKind.Absolute);
            try
            {
                while (new BitmapImage(imageUri) != null)
                {
                    images.Add(imageUri.ToString());
                    imageUri = new Uri("pack://application:,,,/Monopoly.Model;component/Images/Chips/chip-" + counter++.ToString() + ".png", UriKind.Absolute);
                }
            }
            catch (Exception) { }

            Application.Current.Dispatcher.Invoke(() =>
            {
                var dialog = new ImagesDialog();
                var viewModel = new ImagesDialogViewModel(images, (img) =>
                {
                    _dialogCoordinator.HideMetroDialogAsync(this, dialog);
                    parameters.Chip = img;
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
        public IExchangeManager ExchangeManager { get; private set; }
        private IDiceProvider _diceProvider;

        private IDialogCoordinator _dialogCoordinator;
        public IDialogCoordinator DialogCoordinator { get => _dialogCoordinator; set => _dialogCoordinator = value; }

        #endregion
    }
}
