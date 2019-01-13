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
            this.GameManager.MakeDiceRoll();
        }

        private DelegateCommand _nextPlayerCommand;
        public DelegateCommand NextPlayerCommand =>
            _nextPlayerCommand ?? (_nextPlayerCommand = new DelegateCommand(ExecuteNextPlayerCommand).ObservesCanExecute(() => this.GameManager.HaveNotDraws));

        void ExecuteNextPlayerCommand()
        {
            this.GameManager.NextPlayer();
        }

        private DelegateCommand _buildHouseCommand;
        public DelegateCommand BuildHouseCommand =>
            _buildHouseCommand ?? (_buildHouseCommand = new DelegateCommand(ExecuteBuildHouseCommand).ObservesCanExecute(() => this.GameManager.HasMonopoly));

        void ExecuteBuildHouseCommand()
        {
            _eventAggregator.GetEvent<ShowAvailableForBuildingTowns>().Publish();
        }

        private DelegateCommand _stopBuildHouseCommand;
        public DelegateCommand StopBuildHouseCommand =>
            _stopBuildHouseCommand ?? (_stopBuildHouseCommand = new DelegateCommand(ExecuteStopBuildHouseCommand));

        void ExecuteStopBuildHouseCommand()
        {
            _eventAggregator.GetEvent<StopShowAvailableForBuildingTowns>().Publish();
        }

        private DelegateCommand _destroyHouseCommand;
        public DelegateCommand DestroyHouseCommand =>
            _destroyHouseCommand ?? (_destroyHouseCommand = new DelegateCommand(ExecuteDestroyHouseCommand).ObservesCanExecute(() => this.GameManager.HasBuildings));

        void ExecuteDestroyHouseCommand()
        {
            _eventAggregator.GetEvent<ShowAvailableForDestroyingTowns>().Publish();
        }

        private DelegateCommand _stopDestroyHouseCommand;
        public DelegateCommand StopDestroyHouseCommand =>
            _stopDestroyHouseCommand ?? (_stopDestroyHouseCommand = new DelegateCommand(ExecuteStopDestroyHouseCommand));

        void ExecuteStopDestroyHouseCommand()
        {
            _eventAggregator.GetEvent<StopShowAvailableForDestroyingTowns>().Publish();
        }

        private DelegateCommand _mortgageCommand;
        public DelegateCommand MortgageCommand =>
            _mortgageCommand ?? (_mortgageCommand = new DelegateCommand(ExecuteMortgageCommand).ObservesCanExecute(() => this.GameManager.HasTowns));

        void ExecuteMortgageCommand()
        {
            _eventAggregator.GetEvent<ShowAvailableForMortgageTowns>().Publish();
        }

        private DelegateCommand _stopMortgageCommand;
        public DelegateCommand StopMortgageCommand =>
            _stopMortgageCommand ?? (_stopMortgageCommand = new DelegateCommand(ExecuteStopMortgageCommand));

        void ExecuteStopMortgageCommand()
        {
            _eventAggregator.GetEvent<StopShowAvailableForMortgageTowns>().Publish();
        }

        private DelegateCommand _buyFromMortgageCommand;
        public DelegateCommand BuyFromMortgageCommand =>
            _buyFromMortgageCommand ?? (_buyFromMortgageCommand = new DelegateCommand(ExecuteBuyFromMortgageCommand).ObservesCanExecute(() => this.GameManager.HasMortgageCards));

        void ExecuteBuyFromMortgageCommand()
        {
            _eventAggregator.GetEvent<ShowAvailableForBuyFromMortgageTowns>().Publish();
        }

        private DelegateCommand _stopBuyFromMortgageCommand;
        public DelegateCommand StopBuyFromMortgageCommand =>
            _stopBuyFromMortgageCommand ?? (_stopBuyFromMortgageCommand = new DelegateCommand(ExecuteStopBuyFromMortgageCommand));

        void ExecuteStopBuyFromMortgageCommand()
        {
            _eventAggregator.GetEvent<StopShowAvailableForBuyFromMortgageTowns>().Publish();
        }

        private DelegateCommand _tradeCommand;
        public DelegateCommand TradeCommand =>
            _tradeCommand ?? (_tradeCommand = new DelegateCommand(ExecuteTradeCommand));

        void ExecuteTradeCommand()
        {
            _eventAggregator.GetEvent<TradeEvent>().Publish();
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
