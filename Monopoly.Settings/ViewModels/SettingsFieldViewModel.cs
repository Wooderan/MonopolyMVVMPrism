using Monopoly.Model.Abstract;
using Monopoly.Model.Images;
using Monopoly.Model.Interfaces;
using Monopoly.Model.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Monopoly.Model.Model;
using Monopoly.Model.Events;
using System.ComponentModel;

namespace Monopoly.Settings.ViewModels
{
    public class SettingsFieldViewModel : BindableBase, IDataErrorInfo
    {

        #region Constructos

        public SettingsFieldViewModel(IGameManager gameManager, IEventAggregator eventAggregator, IPlayerProvider playerProvider)
        {
            this.StartMoney = 2500;
            _gameManager = gameManager;
            _eventAggregator = eventAggregator;
            _playerProvider = playerProvider;
            _players = new List<PlayerParameters>()
            {
                new PlayerParameters(PlayerType.PLAYER, "Player1", DefaultImagesLocator.GetAvatar("avatar-1.png"), DefaultImagesLocator.GetChip("chip-1.png")),
                new PlayerParameters(PlayerType.COMPUTER, "Computer1", DefaultImagesLocator.GetAvatar("avatar-2.png"), DefaultImagesLocator.GetChip("chip-2.png")),
                new PlayerParameters(PlayerType.COMPUTER, "Computer2", DefaultImagesLocator.GetAvatar("avatar-3.png"), DefaultImagesLocator.GetChip("chip-3.png")),
                new PlayerParameters(PlayerType.COMPUTER, "Computer3", DefaultImagesLocator.GetAvatar("avatar-4.png"), DefaultImagesLocator.GetChip("chip-4.png")),
                new PlayerParameters(PlayerType.COMPUTER, "Computer4", DefaultImagesLocator.GetAvatar("avatar-5.png"), DefaultImagesLocator.GetChip("chip-5.png")),
                new PlayerParameters(PlayerType.COMPUTER, "Computer5", DefaultImagesLocator.GetAvatar("avatar-6.png"), DefaultImagesLocator.GetChip("chip-6.png"))
            };
            this.CurrentTab = 0;
            this.SetCountOfPlayers(2);
            this.Visibility = Visibility.Visible;
        }

        #endregion

        #region Commands

        private DelegateCommand<PlayerParameters> _showAvatarsCommand;
        public DelegateCommand<PlayerParameters> ShowAvatarsCommand =>
            _showAvatarsCommand ?? (_showAvatarsCommand = new DelegateCommand<PlayerParameters>(ExecuteShowAvatarsCommand));

        void ExecuteShowAvatarsCommand(PlayerParameters parameters)
        {
            _eventAggregator.GetEvent<ShowAvatarsEvent>().Publish(parameters);
        }

        private DelegateCommand<PlayerParameters> _showChipsCommand;
        public DelegateCommand<PlayerParameters> ShowChipsCommand =>
            _showChipsCommand ?? (_showChipsCommand = new DelegateCommand<PlayerParameters>(ExecuteShowChipsCommand));

        void ExecuteShowChipsCommand(PlayerParameters parameters)
        {
            _eventAggregator.GetEvent<ShowChipsEvent>().Publish(parameters);
        }

        private DelegateCommand<double?> _showTabCommand;
        public DelegateCommand<double?> ShowTabCommand =>
            _showTabCommand ?? (_showTabCommand = new DelegateCommand<double?>(ExecuteShowTabCommand));

        void ExecuteShowTabCommand(double? parameter)
        {
            this.CurrentTab = parameter != null ? (int)parameter : 0;
        }

        private DelegateCommand _exitCommand;
        public DelegateCommand ExitCommand =>
            _exitCommand ?? (_exitCommand = new DelegateCommand(ExecuteExitCommand));
        void ExecuteExitCommand()
        {
            Application.Current.Shutdown();
        }

        private DelegateCommand<int?> _playersCountCommand;
        public DelegateCommand<int?> PlayersCountCommand =>
            _playersCountCommand ?? (_playersCountCommand = new DelegateCommand<int?>(ExecutePlayersCountCommand));

        void ExecutePlayersCountCommand(int? parameter)
        {
            this.SetCountOfPlayers((int)parameter);
        }

        private DelegateCommand _startCommand;
        public DelegateCommand StartCommand =>
            _startCommand ?? (_startCommand = new DelegateCommand(ExecuteStartCommand));

        void ExecuteStartCommand()
        {
            ObservableCollection<AbstractPlayer> newPlayers = new ObservableCollection<AbstractPlayer>();
            foreach (PlayerParameters pp in this.Players)
            {
                if (pp.Type == PlayerType.PLAYER)
                {
                    newPlayers.Add(new LocalPlayer(pp.Nickname, pp.Avatar, pp.Chip, this.StartMoney));
                }
                else if (pp.Type == PlayerType.COMPUTER)
                {
                    newPlayers.Add(new ComputerPlayer(pp.Nickname, pp.Avatar, pp.Chip, this.StartMoney));
                }
            }
            _playerProvider.UpdatePlayers(newPlayers);
            _gameManager.ReloadGame();
            ExecuteShowTabCommand(0);
            this.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Methods

        private void SetCountOfPlayers(int count)
        {
            if (this.Players is null)
            {
                this.Players = new ObservableCollection<PlayerParameters>();
            }
            else
            {
                this.Players.Clear();
            }
            for (int i = 0; i < count; i++)
            {
                this.Players.Add(_players[i]);
            }
        }

        #endregion

        #region Properties

        private Visibility _visibility;
        public Visibility Visibility
        {
            get { return _visibility; }
            set { SetProperty(ref _visibility, value); }
        }

        private int _currentTab;
        public int CurrentTab
        {
            get { return _currentTab; }
            set { SetProperty(ref _currentTab, value); }
        }

        public ObservableCollection<PlayerParameters> Players { get; set; }

        private int _startMoney;
        public int StartMoney
        {
            get { return _startMoney; }
            set
            {
                SetProperty(ref _startMoney, value);
            }
        }

        public string Error => string.Empty;

        public string this[string columnName] {
            get
            {
                if (columnName == "StartMoney" && this.StartMoney < 1)
                {
                    return "Sum must be positive number";
                }
                return null;
            }
        }

        #endregion

        #region Fields

        private IGameManager _gameManager;
        private IEventAggregator _eventAggregator;
        private IPlayerProvider _playerProvider;
        private List<PlayerParameters> _players;

        #endregion

    }
}
