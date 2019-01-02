using Monopoly.Model.Interfaces;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;

namespace Monopoly.UserField.ViewModels
{
    public class FieldViewModel : BindableBase
    {

        #region Constructor

        public FieldViewModel(IRegionManager regionManager)
        {
            Message = "View A from your Prism Module";
            this.GameManager = (IGameManager)regionManager.Regions["PlayerInfoRegion"].Context;

            this.Players = new ObservableCollection<PlayerViewModel>(this.GameManager.Players.Select(p => new PlayerViewModel(p, this.GameManager)));
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

        public ObservableCollection<PlayerViewModel> Players { get; private set; }

        #endregion
    }
}
