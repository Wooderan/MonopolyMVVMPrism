using System;
using Monopoly.Model.Abstract;
using Monopoly.Model.Interfaces;
using Prism.Mvvm;
using Prism.Regions;

namespace Monopoly.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Constructor
        public MainWindowViewModel(IGameManager gameManager, RegionManager regionManager)
        {
            _regionManager = regionManager;
            this.GameManager = gameManager;
        }
        #endregion

        #region Methods

        private void OpenLeftFlyout(AbstractPlayer player)
        {
            _regionManager.Regions["PlayerDetailsRegion"].Context = player;
            this.IsLeftFlyoutOpened = true;
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
        private RegionManager _regionManager;

        #endregion
    }
}
