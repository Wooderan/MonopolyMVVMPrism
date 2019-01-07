using Monopoly.Model.Abstract;
using Monopoly.Model.Events;
using Monopoly.Model.Interfaces;
using Monopoly.Model.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Monopoly.UserField.ViewModels
{
    public class FieldViewModel : BindableBase
    {

        #region Constructor

        public FieldViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            Message = "View A from your Prism Module";
            this.GameManager = (IGameManager)regionManager.Regions["PlayerInfoRegion"].Context;

            int order = 1;
            this.Players = new ObservableCollection<PlayerViewModel>(this.GameManager.Players.Select(p => new PlayerViewModel(p, order++)));
            _eventAggregator = eventAggregator;
        }

        #endregion

        #region Commands

        private DelegateCommand<AbstractPlayer> _showPlayerDetailsCommand;
        public DelegateCommand<AbstractPlayer> ShowPlayerDetailsCommand =>
            _showPlayerDetailsCommand ?? (_showPlayerDetailsCommand = new DelegateCommand<AbstractPlayer>(ExecuteShowPlayerDetailsCommand));

        void ExecuteShowPlayerDetailsCommand(AbstractPlayer player)
        {
            _eventAggregator.GetEvent<ShowPlayerDetailEvent>().Publish(player);
        }

        private DelegateCommand _makeDrawCommand;
        public DelegateCommand MakeDrawCommand =>
            _makeDrawCommand ?? (_makeDrawCommand = new DelegateCommand(ExecuteMakeDrawCommand).ObservesCanExecute(() => this.GameManager.HaveDraws));

        void ExecuteMakeDrawCommand()
        {
            this.GameManager.MakeDraw();
        }

        private DelegateCommand _nextPlayerCommand;
        public DelegateCommand NextPlayerCommand =>
            _nextPlayerCommand ?? (_nextPlayerCommand = new DelegateCommand(ExecuteNextPlayerCommand).ObservesCanExecute(() => this.GameManager.HaveNotDraws));

        void ExecuteNextPlayerCommand()
        {
            this.GameManager.NextPlayer();
        }

        #endregion

        #region Properties

        private IGameManager _gameManager;
        public IGameManager GameManager
        {
            get { return _gameManager; }
            set { SetProperty(ref _gameManager, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private PlayerViewModel _selectedPlayer;
        public PlayerViewModel SelectedPlayer
        {
            get { return _selectedPlayer; }
            set { SetProperty(ref _selectedPlayer, value); }
        }

        #endregion

        #region Fields

        private IEventAggregator _eventAggregator;

        public ObservableCollection<PlayerViewModel> Players { get; private set; }

        #endregion
    }
}
